using System.ComponentModel.DataAnnotations;

namespace StudyDS_web.Models
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
        public int UserId { get; set; }
        public PassingType? PassingType { get; set; }
    }
}

public enum PassingType
{
    [Display(Name = "Egzamin")]
    exam,
    [Display(Name = "Kolokwium")]
    test,
    [Display(Name = "Prezentacja")]
    prezentation,
    [Display(Name = "Wypracowanie")]
    essay,
}