using StudyDS_web.Models.Validations;

namespace StudyDS_web.Models.ViewModels
{
    public class ChangePasswordViewModel
    {
        public User User { get; set; }
        [User_CorrectNewPassword]
        public string NewPassword { get; set; }
    }
}