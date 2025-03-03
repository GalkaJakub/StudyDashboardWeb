using Study_dashboard_API.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace Study_dashboard_API.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string? Name { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [User_CorrectPassword]
        [Required]
        public string? Password { get; set; }
    }
}
