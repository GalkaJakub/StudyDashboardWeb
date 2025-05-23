﻿using System.ComponentModel.DataAnnotations;

namespace StudyDS_web.Models
{
    // Represents an academic event (e.g. test, project, exam)
    public class Event
    {
        [Required]
        public int EventId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow.Date;
        public PriorityLevelEnum PriorityLevel { get; set; }
        public string? SubjectName { get; set; }
        public EventType? Type { get; set; }
        public int? SubjectId { get; set; }
        public bool IsPassed { get; set; }
        public double? Grade { get; set; }

    }

    // Priority level for events or subjects
    public enum PriorityLevelEnum
    {
        [Display(Name = "niski")]
        Low,
        [Display(Name = "średni")]
        Medium,
        [Display(Name = "wysoki")]
        High,
    }

    // Type of academic event
    public enum EventType
    {
        [Display(Name = "Egzamin")]
        Exam,
        [Display(Name = "Kolokwium")]
        Test,
        [Display(Name = "Projekt")]
        Project,
        [Display(Name = "Prezentacja")]
        Presentation,
        [Display(Name = "Zadanie domowe")]
        Homework,
        [Display(Name = "Sprawozdanie")]
        Report
    }
}
