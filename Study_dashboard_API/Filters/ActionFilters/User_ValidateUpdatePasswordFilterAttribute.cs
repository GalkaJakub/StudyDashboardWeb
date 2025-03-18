using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Study_dashboard_API.Authority;
using Study_dashboard_API.Data;
using Study_dashboard_API.Models;
using Study_dashboard_API.Security;

namespace Study_dashboard_API.Filters.ActionFilters
{
    public class User_ValidateUpdatePasswordFilterAttribute: ActionFilterAttribute
    {
        private readonly ApplicationDbContext db;
        private readonly IPasswordHasher passwordHasher;

        public User_ValidateUpdatePasswordFilterAttribute(ApplicationDbContext db, IPasswordHasher passwordHasher)
        {
            this.db = db;
            this.passwordHasher = passwordHasher;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var model = context.ActionArguments["model"] as ChangePasswordDto;
            var userId = context.HttpContext.Items["UserId"] as int?;
            var user = db.Users.Find(userId);
            if (user == null)
            {
                context.ModelState.AddModelError("Id", "User doesn't exist");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status404NotFound
                };
                context.Result = new NotFoundObjectResult(problemDetails);
            }

            if (!passwordHasher.Verify(user.Password, model.User.Password))
            {
                context.ModelState.AddModelError("Unauthorized", "Błedne hasło");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status401Unauthorized
                };
                context.Result = new UnauthorizedObjectResult(problemDetails);
            }

            context.HttpContext.Items["user"] = user;
        }
    }
}

