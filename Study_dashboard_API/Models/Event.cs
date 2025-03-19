using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Study_dashboard_API.Models
{
    // Represents an event (e.g., exam, test, etc.)
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Required]
        [Range(0, 2)]
        public PriorityLevelEnum PriorityLevel { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public int? UserId { get; set; }
        public int? SubjectId { get; set; }
        public EventType? Type { get; set; }
        public bool IsPassed { get; set; } = false;
        public double? Grade { get; set; }
        [JsonIgnore]
        public User? User { get; set; } // Navigation property to User
        public Subject? Subject { get; set; } // Navigation property to Subject

    }

    // Defines priority levels for events
    public enum PriorityLevelEnum
    {
        Low,
        Medium,
        High
    }
    // Defines possible event types
    public enum EventType
    {
        Exam,
        Test,
        Project,
        Presentation,
        Homework,
        Report
    }
}
