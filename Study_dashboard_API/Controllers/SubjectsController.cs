using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Study_dashboard_API.Data;
using Study_dashboard_API.Filters.ActionFilters;
using Study_dashboard_API.Filters.AuthFilters;
using Study_dashboard_API.Models;

namespace Study_dashboard_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [JwtTokenAuthFilter]
    public class SubjectsController: ControllerBase
    {
        private readonly ApplicationDbContext db;

        public SubjectsController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult getSubjects()
        {
            var userId = HttpContext.Items["UserId"] as int?;
            var subjects = db.Subjects.Where(x=> x.UserId == userId).ToList();
            return Ok(subjects);
        }

        [HttpGet("{subjectId}")]
        [TypeFilter(typeof(Subject_ValidateSubjectIdFilterAttribute))]
        public IActionResult getSubjectById(int subjectId)
        {
            return Ok(HttpContext.Items["subject"]);
        }
        
        [HttpPost]
        [TypeFilter(typeof(Subject_ValidateAddSubjectFilterAttribute))]
        public IActionResult createSubject([FromBody]Subject subject)
        {
            var userId = HttpContext.Items["UserId"] as int?;
            subject.UserId = userId;
            db.Subjects.Add(subject);
            db.SaveChanges();
            return CreatedAtAction(nameof(getSubjectById), new { subjectId = subject.SubjectId }, subject);
        }

        [HttpPut("{subjectId}")]
        [TypeFilter(typeof(Subject_ValidateSubjectIdFilterAttribute))]
        [TypeFilter(typeof(Subject_ValidateUpdateSubjectFilterAttribute))]
        public IActionResult updateSubject(int subjectId, Subject subject)
        {
            var subjectToUpdate = HttpContext.Items["subject"] as Subject;
            subjectToUpdate.PriorityLevel = subject.PriorityLevel;
            subjectToUpdate.Ects = subject.Ects;
            subjectToUpdate.Name = subject.Name;
            subjectToUpdate.PassingType = subject.PassingType;
            db.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{subjectId}")]
        [TypeFilter(typeof(Subject_ValidateSubjectIdFilterAttribute))]
        public IActionResult deleteSubject(int subjectId)
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
