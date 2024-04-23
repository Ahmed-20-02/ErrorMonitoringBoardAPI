namespace DevelopmentProjectErrorBoardAPI.Services
{
    using BCrypt.Net;
    using DevelopmentProjectErrorBoardAPI.Services.Interfaces;

    public class PasswordService : IPasswordService
    {
        public string HashPassword(string password)
        {
            return BCrypt.HashPassword(password);
        }

        // Verify a password against a stored hashed password
        public bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Verify(password, hash);
        }
    }
}