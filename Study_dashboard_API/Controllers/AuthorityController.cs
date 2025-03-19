using Microsoft.AspNetCore.Mvc;
using Study_dashboard_API.Authority;
using Study_dashboard_API.Data;
using Study_dashboard_API.Security;

namespace Study_dashboard_API.Controllers
{
    // API controller for authentication requests
    [ApiController]
    public class AuthorityController: ControllerBase
    {

        private readonly IConfiguration configuration;
        private readonly ApplicationDbContext db;
        private readonly IPasswordHasher passwordHasher;

        public AuthorityController(IConfiguration configuration, ApplicationDbContext db, IPasswordHasher passwordHasher)
        {
            this.configuration = configuration;
            this.db = db;
            this.passwordHasher = passwordHasher;
        }

        // POST: /auth
        // Authenticates a user and returns a JWT token if successful
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
                else if (passwordHasher.Verify(user.Password, appCredencial.UserPassword))
                {
                    var expiresAt = DateTime.UtcNow.AddHours(2);
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
