using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Study_dashboard_API.Models
{
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public int? Ects { get; set; }
        [Required]
        [Range(0, 2)]
        public PriorityLevelEnum PriorityLevel { get; set; }
        public int? UserId { get; set; }
        public PassingType? PassingType { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
    }

    public enum PassingType
    {
        exam,
        test,
        prezentation,
        essay,
    }
}
