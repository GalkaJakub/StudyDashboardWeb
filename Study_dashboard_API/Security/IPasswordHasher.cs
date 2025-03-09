namespace Study_dashboard_API.Security
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool Verify(string passwordHash, string inputPassword);
    }
}
