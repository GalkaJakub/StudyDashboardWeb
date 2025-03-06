using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Study_dashboard_API.Data;
using Study_dashboard_API.Filters.ActionFilters;
using Study_dashboard_API.Models;

namespace Study_dashboard_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController: ControllerBase
    {
        private readonly ApplicationDbContext db;

        public EventsController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public ActionResult getEvents()
        {
            return Ok(db.Events.ToList());
        }

        [HttpGet("eventId")]
        [TypeFilter(typeof(Event_ValidateEventIdFilterAttribute))]
        public ActionResult getEventById(int eventId)
        {
            var ev = HttpContext.Items["event"] as Event;
            return Ok(ev);
        }

        [HttpPost]
        [TypeFilter(typeof(Event_ValidateAddEventFilterAttribute))]
        public ActionResult createEvent([FromBody]Event ev)
        {
            db.Events.Add(ev);
            db.SaveChanges();
            return CreatedAtAction("getEventById", new {eventId = ev.EventId}, ev);
        }

        [HttpPut("eventId")]
        [TypeFilter(typeof(Event_ValidateEventIdFilterAttribute))]
        [TypeFilter(typeof(Event_ValidateUpdateEventFilterAttribute))]
        public ActionResult updateEvent(int eventId, Event ev)
        {
            var evToUpdate = HttpContext.Items["event"] as Event;
            evToUpdate.Name = ev.Name;
            evToUpdate.Description = ev.Description;
            evToUpdate.PriorityLevel = ev.PriorityLevel;
            evToUpdate.SubjectId = ev.SubjectId;
            evToUpdate.Date = ev.Date;
            db.SaveChanges();
            return NoContent();
        }

        [HttpDelete("eventId")]
        [TypeFilter(typeof(Event_ValidateEventIdFilterAttribute))]
        public ActionResult deleteEvent(int eventId)
        {
            var ev = HttpContext.Items["event"] as Event;
            db.Events.Remove(ev);
            db.SaveChanges();
            return Ok(ev);
        }
    }
}
