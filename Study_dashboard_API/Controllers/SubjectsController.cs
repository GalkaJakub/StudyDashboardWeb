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
    // Controller responsible for handling subject-related API requests
    [ApiController]
    [Route("api/[controller]")]
    [JwtTokenAuthFilter]// Custom JWT authentication filter
    public class SubjectsController: ControllerBase
    {
        private readonly ApplicationDbContext db;

        public SubjectsController(ApplicationDbContext db)
        {
            this.db = db;
        }

        // GET: api/subjects
        // Returns all subjects for the currently authenticated user
        [HttpGet]
        public IActionResult getSubjects()
        {
            var userId = HttpContext.Items["UserId"] as int?;
            var subjects = db.Subjects.Where(x=> x.UserId == userId).ToList();
            return Ok(subjects);
        }

        // GET: api/subjects/{subjectId}
        // Returns a specific subject by its ID
        [HttpGet("{subjectId}")]
        [TypeFilter(typeof(Subject_ValidateSubjectIdFilterAttribute))]
        public IActionResult getSubjectById(int subjectId)
        {
            return Ok(HttpContext.Items["subject"]);
        }

        // POST: api/subjects
        // Creates a new subject for the user
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

        // PUT: api/subjects/{subjectId}
        // Updates an existing subject's details
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
            subjectToUpdate.IsPassed = subject.IsPassed;
            subjectToUpdate.Grade = subject.Grade;
            db.SaveChanges();

            return NoContent();
        }

        // DELETE: api/subjects/{subjectId}
        // Deletes a subject and detaches related events
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
