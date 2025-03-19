using System.ComponentModel.DataAnnotations;

namespace StudyDS_web.Models
{
    // Represents a subject assigned to a user
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }
        [Required(ErrorMessage = "Nazwa przedmiotu jest wymagana.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Ilość punktów ECTS jest wymagana.")]
        public int? Ects { get; set; }
        [Required(ErrorMessage = "Priorytet jest wymagany.")]
        [Range(0, 2, ErrorMessage = "Priorytet musi zawierać się w przedziale od 0 do 2.")]
        public PriorityLevelEnum PriorityLevel { get; set; }
        public int UserId { get; set; }
        public PassingType? PassingType { get; set; }
        public bool IsPassed { get; set; }
        public double? Grade { get; set; }
    }
}

// Enum representing the type of passing method for a subject
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