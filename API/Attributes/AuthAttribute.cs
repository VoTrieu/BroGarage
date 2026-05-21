using BroGarage.API.Models;
using BroGarage.API.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BroGarage.API.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // skip authorization if action is decorated with [AllowAnonymous] attribute
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;

        // authorization
        var user = (UserJwtModel?)context.HttpContext.Items["User"];
        if (user == null)
        {
            var response = new ResponseModel()
            {
                Message = "Unauthorized",
                Code = 401
            };
            context.Result = new JsonResult(response)
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };
        }
    }
}
