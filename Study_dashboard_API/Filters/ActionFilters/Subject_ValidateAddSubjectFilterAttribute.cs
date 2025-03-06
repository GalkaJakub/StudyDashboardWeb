using Microsoft.AspNetCore.Mvc.Filters;
using Study_dashboard_API.Data;
using Study_dashboard_API.Models;

namespace Study_dashboard_API.Filters.ActionFilters
{
    public class Subject_ValidateAddSubjectFilterAttribute: ActionFilterAttribute
    {
        private readonly ApplicationDbContext db;

        public Subject_ValidateAddSubjectFilterAttribute(ApplicationDbContext db)
        {
            this.db = db;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var subject = context.HttpContext.Items["subject"] as Subject;
        }
    }
}
