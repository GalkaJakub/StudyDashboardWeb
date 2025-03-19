namespace Study_dashboard_API.Models
{
    // Model used to change user password
    public class ChangePasswordDto
    {
        // Current user (used for old password check)
        public User User { get; set; }

        // New password to be saved
        public string NewPassword { get; set; }
    }


}
