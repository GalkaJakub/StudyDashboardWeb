﻿namespace Study_dashboard_API.Models
{
    public class EventDto
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public PriorityLevelEnum PriorityLevel { get; set; }
        public string? SubjectName { get; set; }
        public bool IaActive { get; set; } = true;
    }

}
