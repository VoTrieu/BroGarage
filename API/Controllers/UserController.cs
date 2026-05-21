using BroGarage.API.Attributes;
using BroGarage.API.Models;
using BroGarage.API.Utilities;
using BroGarage.API.Data;
using BroGarage.API.Data.Entities;
using BroGarage.API.Shared.Models;
using BroGarage.API.Shared.RequestModels.User;
using BroGarage.API.Shared.ResponseModels.User;
using BroGarage.API.Shared.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BroGarage.API.Controllers;

[Route("user")]
public class UserController : BaseController
{
    private readonly JwtUtility jwtUtility;
    private readonly IConfiguration configuration;

    public UserController(DatabaseContext db, JwtUtility jwtUtility, IConfiguration configuration) : base(db)
    {
        this.jwtUtility = jwtUtility;
        this.configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<ActionResult<ResponseModel<UserLoginResModel>>> Login(UserLoginReqModel req)
    {
        ResponseModel response = new();

        var query = await db.Users
            .Where(n => n.UserName == req.UserName)
            .Select(n => new
            {
                n.UserId,
                n.UserName,
                n.FullName,
                n.Salt,
                n.PasswordHash
            })
            .FirstOrDefaultAsync();

        const string invalidUser = "Tài khoản hoặc mật khẩu không đúng";

        if (query == null)
        {
            response.Message = invalidUser;
            return Ok(response);
        }

        string passwordHash = CryptographyUtility.HashSHA256(string.Concat(req.Password, query.Salt));

        if (passwordHash != query.PasswordHash)
        {
            response.Message = invalidUser;
            return Ok(response);
        }

        var jwtResponse = jwtUtility.GenerateToken(new UserJwtModel()
        {
            UserId = query.UserId,
            UserName = query.UserName ?? "",
            FullName = query.FullName ?? ""
        });

        var refreshTokenDurationInDate = configuration.GetValue<int>("RefreshTokenDurationInDay");

        var expirationInDate = DateTime.Now.AddDays(refreshTokenDurationInDate);

        await db.RefreshTokens.AddAsync(new RefreshTokenEntity()
        {
            UserId = query.UserId,
            RefreshToken = jwtResponse.RefreshToken,
            ExpirationInDate = expirationInDate
        });

        await db.SaveChangesAsync();

        response.Result = new UserLoginResModel()
        {
            UserName = query.UserName ?? "",
            FullName = query.FullName ?? "",
            AccessToken = jwtResponse.AccessToken,
            RefreshToken = jwtResponse.RefreshToken,
            ExpirationTime = jwtResponse.ExpirationTime
        };

        response.IsSuccess = true;

        return Ok(response);
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<ResponseModel<UserRefreshTokenResModel>>> RefreshToken(UserRefreshTokenReqModel req)
    {
        ResponseModel<UserRefreshTokenResModel> response = new();

        var now = DateTime.Now;

        var query = await db.RefreshTokens
            .Where(n => n.RefreshToken == req.RefreshToken && n.ExpirationInDate > now.Date)
            .FirstOrDefaultAsync();

        if (query == null)
        {
            response.Message = "Refresh token không hợp lệ";
            return Ok(response);
        }

        var refreshTokenDurationInDate = configuration.GetValue<int>("RefreshTokenDurationInDay");

        var expirationInDate = DateTime.Now.AddDays(refreshTokenDurationInDate);

        var user = await db.Users
            .Where(n => n.UserId == query.UserId)
            .Select(n => new
            {
                n.UserId,
                n.UserName,
                n.FullName
            })
            .FirstOrDefaultAsync();

        var jwtResponse = jwtUtility.GenerateToken(new UserJwtModel()
        {
            UserId = user?.UserId ?? 0,
            UserName = user?.UserName ?? "",
            FullName = user?.FullName ?? ""
        });

        using var tran = await db.Database.BeginTransactionAsync();

        db.RefreshTokens.Remove(query);

        await db.SaveChangesAsync();

        await db.RefreshTokens
            .AddAsync(new RefreshTokenEntity()
            {
                UserId = user?.UserId ?? 0,
                RefreshToken = jwtResponse.RefreshToken,
                ExpirationInDate = expirationInDate
            });

        await db.SaveChangesAsync();

        await tran.CommitAsync();

        response.Result = new UserRefreshTokenResModel()
        {
            UserName = user?.UserName ?? "",
            FullName = user?.FullName ?? "",
            AccessToken = jwtResponse.AccessToken,
            RefreshToken = jwtResponse.RefreshToken,
            ExpirationTime = jwtResponse.ExpirationTime
        };

        response.IsSuccess = true;

        return Ok(response);
    }

    [HttpPut("change-password")]
    [Auth]
    public async Task<ActionResult<ResponseModel>> ChangePassword(UserChangePasswordReqModel req)
    {
        ResponseModel response = new();

        var query = await db.Users
            .Where(n => n.UserId == USER_ID)
            .AsTracking()
            .FirstOrDefaultAsync();

        if (query == null)
        {
            response.Message = NOT_FOUND_MSG;
            return Ok(response);
        }

        string passwordHash = CryptographyUtility.HashSHA256(string.Concat(req.CurrentPassword, query.Salt));

        if (passwordHash != query.PasswordHash)
        {
            response.Message = "Mật khẩu hiện tại không đúng";
            return Ok(response);
        }

        string newSalt = StringUtility.Random(128);

        string newPasswordHash = CryptographyUtility.HashSHA256(string.Concat(req.NewPassword, newSalt));

        query.Salt = newSalt;
        query.PasswordHash = newPasswordHash;

        await db.SaveChangesAsync();

        response.IsSuccess = true;

        return Ok(response);
    }

    [HttpDelete("refresh-token/revoke")]
    [Auth]
    public async Task<ActionResult<ResponseModel>> RevokeRefreshToken()
    {
        ResponseModel response = new();

        var refreshTokens = await db.RefreshTokens
            .Where(n => n.UserId == USER_ID)
            .ToArrayAsync();

        db.RefreshTokens.RemoveRange(refreshTokens);

        await db.SaveChangesAsync();

        response.IsSuccess = true;

        return Ok(response);
    }
}
