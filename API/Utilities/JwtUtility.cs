using BroGarage.API.Models;
using BroGarage.API.Shared.Utilities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BroGarage.API.Utilities;

public class JwtUtility
{
    private readonly IConfiguration configuration;

    public JwtUtility(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public JwtResModel GenerateToken(UserJwtModel user)
    {
        string secretKey = configuration.GetValue<string>("SecretKey")
            ?? throw new InvalidOperationException("Missing SecretKey configuration.");
        int accessTokenDurationInMinute = configuration.GetValue<int>("AccessTokenDurationInMinute");

        var expiredDate = DateTime.UtcNow.AddMinutes(accessTokenDurationInMinute);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {
            new Claim("id", user.UserId.ToString()),
            new Claim("user", user.UserName),
            new Claim("name", user.UserName),
        }),
            Expires = expiredDate,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        string refreshToken = StringUtility.Random(50);

        var accessToken = tokenHandler.WriteToken(token);

        return new JwtResModel()
        {
            AccessToken = accessToken,
            ExpirationTime = (long)expiredDate.Subtract(new DateTime(1970, 1, 1)).TotalSeconds,
            RefreshToken = refreshToken
        };
    }

    public UserJwtModel? ValidateToken(string token)
    {
        if (token == null)
            return null;

        string secretKey = configuration.GetValue<string>("SecretKey")
            ?? throw new InvalidOperationException("Missing SecretKey configuration.");

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secretKey);
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var id = jwtToken.Claims.First(n => n.Type == "id").Value ?? "0";
            var userName = jwtToken.Claims.First(n => n.Type == "user").Value ?? "";
            var fullName = jwtToken.Claims.First(n => n.Type == "name").Value ?? "";

            // return user id from JWT token if validation successful
            return new UserJwtModel()
            {
                UserId = Convert.ToInt32(id),
                UserName = userName,
                FullName = fullName
            };
        }
        catch
        {
            // return null if validation fails
            return null;
        }
    }
}
