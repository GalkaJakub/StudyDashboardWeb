using Microsoft.AspNetCore.Mvc;
using Study_dashboard_API.Authority;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.Collections.Generic;

namespace Study_dashboard_API.Controllers
{
    [ApiController]
    public class AuthorityController: ControllerBase
    {
        //IConfiguration pobiera informacje z ustawien konfiguracyjnych aplikacji np. z appsettings.json 
        private readonly IConfiguration configuration;

        public AuthorityController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost("auth")]
        public IActionResult Authenicate([FromBody]AppCredencial appCredencial)
        {
            if (Authenticator.Authenticate(appCredencial.ClientId, appCredencial.Secret))
            {
                var expiresAt = DateTime.UtcNow.AddMinutes(10);
                return Ok(new
                {
                    access_token = Authenticator.CreateToken(appCredencial.ClientId, expiresAt, configuration.GetValue<string>("SecretKey")),
                    expires_at = expiresAt
                });
            }
            else
            {
                ModelState.AddModelError("Unauthorized", "You are not authorized");
                var problemDetails = new ValidationProblemDetails(ModelState)
                {
                    Status = StatusCodes.Status401Unauthorized
                };
                return new UnauthorizedObjectResult(problemDetails);
            }
        }
    }
}
