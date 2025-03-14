﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Study_dashboard_API.Models
{
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
        public EventType Type { get; set; } = EventType.Exam;
        public bool IaActive { get; set; } = true;
        [JsonIgnore]
        public User? User { get; set; }
        public Subject? Subject { get; set; }
    }

    public enum PriorityLevelEnum
    {
        Low = 0,
        Medium = 1,
        High = 2
    }

    public enum EventType
    {
        Exam = 0,
        Test = 1,
        Project = 2,
        Presentation = 3,
    }
}
