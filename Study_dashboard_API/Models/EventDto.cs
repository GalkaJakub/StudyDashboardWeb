namespace Study_dashboard_API.Models
{
    // Data Transfer Object used to send event data to the client
    public class EventDto
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public PriorityLevelEnum PriorityLevel { get; set; }
        public string? SubjectName { get; set; }
        public bool IsPassed { get; set; } = false;
        public EventType? Type { get; set; }
        public double? Grade { get; set; }
    }

}
