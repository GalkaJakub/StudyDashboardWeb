using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Study_dashboard_API.Data;
using Study_dashboard_API.Models;

namespace Study_dashboard_API.Filters.ActionFilters
{
    public class Event_ValidateEventIdFilterAttribute: ActionFilterAttribute
    {
        private readonly ApplicationDbContext db;

        public Event_ValidateEventIdFilterAttribute(ApplicationDbContext db)
        {
            this.db = db;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var eventId = context.ActionArguments["EventId"] as int?;

            if (eventId != null)
            {
                if (eventId < 0)
                {
                    context.ModelState.AddModelError("EventId", "EventId is inavlid");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                }
                else
                {
                    var validEv = db.Events.Find(eventId);
                    if (validEv != null)
                    {
                        context.HttpContext.Items["event"] = validEv;
                    }
                    else
                    {
                        context.ModelState.AddModelError("EventId", "Event doesn't exist");
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
