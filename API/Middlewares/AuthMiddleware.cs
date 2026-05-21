using BroGarage.API.Models;
using BroGarage.API.Utilities;

namespace BroGarage.API.Middlewares;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;

    public AuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, JwtUtility jwtUtils)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token != null)
        {
            var user = jwtUtils.ValidateToken(token);
            if (user != null)
            {
                // attach user to context on successful jwt validation
                context.Items["User"] = new UserJwtModel()
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    FullName = user.FullName
                };
            }
        }

        await _next(context);
    }
}
