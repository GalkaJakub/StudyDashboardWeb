using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Study_dashboard_API.Data;
using Study_dashboard_API.Models;

namespace Study_dashboard_API.Filters.ActionFilters
{
    public class Subject_ValidateSubjectIdFilterAttribute: ActionFilterAttribute
    {
        private readonly ApplicationDbContext db;

        public Subject_ValidateSubjectIdFilterAttribute(ApplicationDbContext db)
        {
            this.db = db;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var subjectId = context.ActionArguments["subjectId"] as int?;

            if (subjectId.HasValue)
            {
                if (subjectId.Value < 0)
                {
                    context.ModelState.AddModelError("SubjectId", "Id is invalid");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                }
                else
                {
                    var subject = db.Subjects.Find(subjectId.Value);
                    if (subject == null)
                    {
                        context.ModelState.AddModelError("SubjectId", "Subject doesn't exist");
                        var problemDetails = new ValidationProblemDetails(context.ModelState)
                        {
                            Status = StatusCodes.Status404NotFound
                        };
                        context.Result = new NotFoundObjectResult(problemDetails);
                    }
                    else
                    {
                        context.HttpContext.Items["subject"] = subject;
                    }
                }
            }
        }


    }
}