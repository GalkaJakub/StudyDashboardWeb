using System.ComponentModel.DataAnnotations;

namespace StudyDS_web.Models.Validations
{
    public class User_CorrectPasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var user = validationContext.ObjectInstance as User;

            if (user != null && !string.IsNullOrWhiteSpace(user.Password))
            {
                if (user.Password.Length < 8)
                {
                    return new ValidationResult("Hasło musi posiadać co najmniej 8 znaków.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
