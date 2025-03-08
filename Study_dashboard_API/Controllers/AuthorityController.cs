using Microsoft.AspNetCore.Mvc;
using Study_dashboard_API.Authority;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.Collections.Generic;
using Study_dashboard_API.Data;

namespace Study_dashboard_API.Controllers
{
    [ApiController]
    public class AuthorityController: ControllerBase
    {
        //IConfiguration pobiera informacje z ustawien konfiguracyjnych aplikacji np. z appsettings.json 
        private readonly IConfiguration configuration;
        private readonly ApplicationDbContext db;

        public AuthorityController(IConfiguration configuration, ApplicationDbContext db)
        {
            this.configuration = configuration;
            this.db = db;
        }

        [HttpPost("auth")]
        public IActionResult Authenicate([FromBody]AppCredencial appCredencial)
        {
            if (Authenticator.Authenticate(appCredencial.ClientId, appCredencial.Secret))
            {
                var user = db.Users.FirstOrDefault(x => x.Name == appCredencial.UserName);
                if (user == null)
                {
                    ModelState.AddModelError("Unauthorized", "User not't found");
                    var problemDetails = new ValidationProblemDetails(ModelState)
                    {
                        Status = StatusCodes.Status401Unauthorized
                    };
                    return new UnauthorizedObjectResult(problemDetails);
                }
                else if (user.Password == appCredencial.UserPassword)
                {
                    var expiresAt = DateTime.UtcNow.AddMinutes(2);
                    string userId = user.UserId.ToString();
                    return Ok(new
                    {
                        access_token = Authenticator.CreateToken(appCredencial.ClientId, userId, expiresAt, configuration.GetValue<string>("SecretKey")),
                        expires_at = expiresAt
                    });
                }
                else
                {
                    ModelState.AddModelError("Unauthorized", "Incorrect password");
                    var problemDetails = new ValidationProblemDetails(ModelState)
                    {
                        Status = StatusCodes.Status401Unauthorized
                    };
                    return new UnauthorizedObjectResult(problemDetails);
                }

            }
            else
            {
                ModelState.AddModelError("Unauthorized", "Your application is not authorized");
                var problemDetails = new ValidationProblemDetails(ModelState)
                {
                    Status = StatusCodes.Status401Unauthorized
                };
                return new UnauthorizedObjectResult(problemDetails);
            }
        }
    }
}
