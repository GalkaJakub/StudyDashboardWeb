using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Study_dashboard_API.Data;
using Study_dashboard_API.Models.Repositories;

namespace Study_dashboard_API.Filters.ExceptionFilters
{
    public class User_HandleUpdateUserExceptionFilterAttribute: ExceptionFilterAttribute
    {
        private readonly ApplicationDbContext db;

        public User_HandleUpdateUserExceptionFilterAttribute(ApplicationDbContext db)
        {
            this.db = db;
        }
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            var userId = context.RouteData.Values["id"] as string;
            if (int.TryParse(userId, out int userIdValue))
            {
               if (db.Users.FirstOrDefault(x => x.UserId == userIdValue) == null)
                {
                    context.ModelState.AddModelError("UserId", "User doesn't exist");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status404NotFound
                    };
                    context.Result = new NotFoundObjectResult(problemDetails);
                }
            }
        }
    }
}
