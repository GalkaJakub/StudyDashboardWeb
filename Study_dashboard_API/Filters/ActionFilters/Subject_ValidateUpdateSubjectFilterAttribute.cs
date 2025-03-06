using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Study_dashboard_API.Models;

namespace Study_dashboard_API.Filters.ActionFilters
{
    public class Subject_ValidateUpdateSubjectFilterAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var subjectId = context.ActionArguments["subjectId"] as int?;
            var subject = context.ActionArguments["subject"] as Subject;

            if (subject != null && subjectId.HasValue && subjectId != subject.SubjectId)
            {
                context.ModelState.AddModelError("Subject", "Id and SubjectId are not the same");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
        }
    }
}
