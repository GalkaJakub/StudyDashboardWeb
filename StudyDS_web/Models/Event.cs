using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace StudyDS_web.Models
{
    public class Event
    {
        [Required]
        public int EventId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow.Date;
        public PriorityLevelEnum PriorityLevel { get; set; }
        public string? SubjectName { get; set; }
        public bool IaActive { get; set; } = true;
        //public EventType Type { get; set; } = EventType.Exam;
        public int? SubjectId { get; set; }

    }

    public enum PriorityLevelEnum
    {
        [Display(Name = "niski")]
        Low = 0,
        [Display(Name = "średni")]
        Medium = 1,
        [Display(Name = "wysoki")]
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
