using StudyDS_web.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace StudyDS_web.Models
{
    // Represents a user in the system
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Nazwa użytkownika jest wymagana.")]
        public string? Name { get; set; }
        [EmailAddress(ErrorMessage = "Wprowadź poprawny adres e-mail.")]
        public string? Email { get; set; }
        [User_CorrectPassword]
        [Required(ErrorMessage = "Hasło jest wymagane.")]
        public string? Password { get; set; }
    }
}
