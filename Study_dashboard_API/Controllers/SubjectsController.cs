using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Study_dashboard_API.Data;
using Study_dashboard_API.Filters.ActionFilters;
using Study_dashboard_API.Models;

namespace Study_dashboard_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectsController: ControllerBase
    {
        private readonly ApplicationDbContext db;

        public SubjectsController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpGet("user/{id}")]
        [TypeFilter(typeof(User_ValidateUserIdFilterAttribute))]
        public IActionResult GetSubjects(int id)
        {
            var events = db.Subjects.Where(x => x.UserId == id).ToList();
            return Ok(events);
        }

        [HttpGet("{subjectId}")]
        [TypeFilter(typeof(Subject_ValidateSubjectIdFilterAttribute))]
        public IActionResult GetSubjectById(int subjectId)
        {
            return Ok(HttpContext.Items["subject"]);
        }
        
        [HttpPost]
        public IActionResult CreateSubject([FromBody]Subject subject)
        {
            db.Subjects.Add(subject);
            db.SaveChanges();
            return Ok();
        }

        [HttpPut("{subjectId}")]
        public IActionResult UpdateSubject(int subjectId, Subject subject)
        {
            return Ok();
        }

        [HttpDelete("{subjectId}")]
        [TypeFilter(typeof(Subject_ValidateSubjectIdFilterAttribute))]
        public IActionResult DeleteSubject(int subjectId)
        {
            var subject = HttpContext.Items["subject"] as Subject;
            var events = db.Events.Where(x => x.SubjectId == subjectId).ToList();
            if (events.IsNullOrEmpty())
            {
                foreach (var ev in events)
                {
                    ev.SubjectId = null;
                    ev.Subject = null;
                }
            }
            db.Subjects.Remove(subject);
            db.SaveChanges();
            return Ok(subject);
        }
    }
}
