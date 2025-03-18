using StudyDS_web.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace StudyDS_web.Models.Validations
{
    public class User_CorrectNewPasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var model = validationContext.ObjectInstance as ChangePasswordViewModel;

            if (!string.IsNullOrWhiteSpace(model.NewPassword))
            {
                if (model.NewPassword.Length < 8)
                {
                    return new ValidationResult("Hasło musi posiadać co najmniej 8 znaków.");
                }
            }

            return ValidationResult.Success;
        }
    }
}