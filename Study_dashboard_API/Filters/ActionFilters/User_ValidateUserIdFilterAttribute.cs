using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Study_dashboard_API.Data;

namespace Study_dashboard_API.Filters.ActionFilters
{
    public class User_ValidateUserIdFilterAttribute : ActionFilterAttribute
    {
        private readonly ApplicationDbContext db;

        public User_ValidateUserIdFilterAttribute(ApplicationDbContext db) 
        {
            this.db = db;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var userId = context.ActionArguments["id"] as int?;

            if (userId.HasValue)
            {
                if (userId.Value < 0)
                {
                    context.ModelState.AddModelError("Id", "Id is inavlid");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                }
                else
                {
                    var user = db.Users.Find(userId.Value);
                    if (user == null)
                    {
                        context.ModelState.AddModelError("Id", "User doesn't exist");
                        var problemDetails = new ValidationProblemDetails(context.ModelState)
                        {
                            Status = StatusCodes.Status404NotFound
                        };
                        context.Result = new NotFoundObjectResult(problemDetails);
                    }
                    else
                    {
                        context.HttpContext.Items["user"] = user;
                    }
                }
            }
        }
    }
}
