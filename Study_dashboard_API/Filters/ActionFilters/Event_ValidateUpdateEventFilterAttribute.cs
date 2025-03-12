using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Study_dashboard_API.Data;
using Study_dashboard_API.Models;

namespace Study_dashboard_API.Filters.ActionFilters
{
    public class Event_ValidateUpdateEventFilterAttribute: ActionFilterAttribute
    {
        private readonly ApplicationDbContext db;

        public Event_ValidateUpdateEventFilterAttribute(ApplicationDbContext db)
        {
            this.db = db;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var eventId = context.ActionArguments["eventId"] as int?;
            var ev = context.ActionArguments["ev"] as Event;
            var userId = context.HttpContext.Items["UserId"] as int?;

            if (eventId.HasValue && ev != null && eventId != ev.EventId)
            {
                context.ModelState.AddModelError("Event", "Id and EventId are not the same");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }

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
