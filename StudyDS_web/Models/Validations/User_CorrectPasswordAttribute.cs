using Microsoft.AspNetCore.Http.HttpResults;
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
                    return new ValidationResult("Password has to be longer than 7 char.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
