using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Study_dashboard_API.Authority;
using Study_dashboard_API.Data;
using System.IdentityModel.Tokens.Jwt;

namespace Study_dashboard_API.Filters.AuthFilters
{
    public class JwtTokenAuthFilterAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if(!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();
            var jwtToken = Authenticator.VerifyToken(token, configuration.GetValue<string>("SecretKey"));
            if (jwtToken == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            
            var userId = jwtToken.Claims.FirstOrDefault(x=> x.Type == "UserId")?.Value;
            if (int.TryParse(userId, out var intUserId))
            {
                context.HttpContext.Items["UserId"] = intUserId;
            }
        }
    }
}
