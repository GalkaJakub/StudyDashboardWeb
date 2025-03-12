using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Study_dashboard_API.Data;
using Study_dashboard_API.Models;

namespace Study_dashboard_API.Filters.ActionFilters
{
    public class Event_ValidateAddEventFilterAttribute: ActionFilterAttribute
    {
        private readonly ApplicationDbContext db;

        public Event_ValidateAddEventFilterAttribute(ApplicationDbContext db)
        {
            this.db = db;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var ev = context.ActionArguments["ev"] as Event;
            var userId = context.HttpContext.Items["UserId"] as int?;

            if (ev == null)
            {
                context.ModelState.AddModelError("Event", "Event is null");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
            else
            {
                if (ev.SubjectId != null)
                {
                    var validateSubject = db.Subjects.FirstOrDefault(x => x.SubjectId == ev.SubjectId && x.UserId == userId);
                    if (validateSubject == null)
                    {
                        context.ModelState.AddModelError("Event", "Subject doesn't exist");
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
}
