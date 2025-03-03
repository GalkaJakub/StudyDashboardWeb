using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Study_dashboard_API.Models;

namespace Study_dashboard_API.Filters.ActionFilters
{
    public class User_ValidateUpdateUserFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var id = context.ActionArguments["id"] as int?;
            var user = context.ActionArguments["user"] as User;

            if (id.HasValue && user != null && id != user.UserId)
            {
                context.ModelState.AddModelError("User", "Id and UserId are not the same");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
        }
    }
}
