using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Study_dashboard_API.Models
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
        public int PriorityLevel { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public int UserId { get; set; }
        public int? SubjectId { get; set; }

        public User? User { get; set; }
        public Subject? Subject { get; set; }
    }
}
