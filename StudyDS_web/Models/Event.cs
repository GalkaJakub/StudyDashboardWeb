using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace StudyDS_web.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(0, 2)]
        public PriorityLevelEnum PriorityLevel { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public int UserId { get; set; }
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

}
