﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Study_dashboard_API.Data;
using Study_dashboard_API.Filters.ActionFilters;
using Study_dashboard_API.Filters.AuthFilters;
using Study_dashboard_API.Models;

namespace Study_dashboard_API.Controllers
{
    // Controller responsible for handling event-related API requests
    [ApiController]
    [Route("api/[controller]")]
    [JwtTokenAuthFilter]// Custom JWT authentication filter
    public class EventsController: ControllerBase
    {
        private readonly ApplicationDbContext db;

        public EventsController(ApplicationDbContext db)
        {
            this.db = db;
        }

        // GET: api/events
        // Returns all events for the authenticated user
        [HttpGet]
        public ActionResult getEvents()
        {
            var userId = HttpContext.Items["UserId"] as int?;

            var events = db.Events
            .Where(x => x.UserId == userId)
            .Include(e => e.Subject)
            .ToList();

            var eventDtos = events.Select(e => new EventDto
            {
                EventId = e.EventId,
                Name = e.Name,
                Description = e.Description,
                Date = e.Date,
                PriorityLevel = e.PriorityLevel,
                SubjectName = e.Subject?.Name,
                IsPassed = e.IsPassed,
                Type = e.Type,
                Grade = e.Grade
            }).ToList();

            return Ok(eventDtos);
        }

        // GET: api/events/{eventId}
        // Returns a specific event by ID
        [HttpGet("{eventId}")]
        [TypeFilter(typeof(Event_ValidateEventIdFilterAttribute))]
        public ActionResult getEventById(int eventId)
        {
            return Ok(HttpContext.Items["event"]);
        }

        // POST: api/events
        // Creates a new event for the current user
        [HttpPost]
        [TypeFilter(typeof(Event_ValidateAddEventFilterAttribute))]
        public ActionResult createEvent([FromBody]Event ev)
        {
            var userId = HttpContext.Items["UserId"] as int?;
            ev.UserId = userId;
            db.Events.Add(ev);
            db.SaveChanges();
            return CreatedAtAction("getEventById", new {eventId = ev.EventId}, ev);
        }

        // PUT: api/events/{eventId}
        // Updates an existing event
        [HttpPut("{eventId}")]
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
            evToUpdate.Type = ev.Type;
            evToUpdate.IsPassed = ev.IsPassed;
            evToUpdate.Grade = ev.Grade;
            db.SaveChanges();
            return NoContent();
        }

        // DELETE: api/events/{eventId}
        // Deletes an event by ID
        [HttpDelete("{eventId}")]
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
