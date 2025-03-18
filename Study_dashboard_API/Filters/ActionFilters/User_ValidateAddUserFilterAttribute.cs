using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Study_dashboard_API.Data;
using Study_dashboard_API.Models;

namespace Study_dashboard_API.Filters.ActionFilters
{
    public class User_ValidateAddUserFilterAttribute : ActionFilterAttribute
    {
        private readonly ApplicationDbContext db;

        public User_ValidateAddUserFilterAttribute(ApplicationDbContext db)
        {
            this.db = db;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var user = context.ActionArguments["user"] as User;
            if (user == null)
            {
                context.ModelState.AddModelError("User", "User is null");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
            else
            {
                var validateUser = db.Users.FirstOrDefault(x => x.Name.ToLower() == user.Name.ToLower());
                if (validateUser != null)
                {
                    context.ModelState.AddModelError("User", "Uzytkownik juz istnieje.");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                }
                validateUser = db.Users.FirstOrDefault(x => x.Email ==  user.Email);
                if (validateUser != null && validateUser.Email != null)
                {
                    context.ModelState.AddModelError("User", "Uzytkownik juz istnieje.");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                }
            }


        }
    }
}
