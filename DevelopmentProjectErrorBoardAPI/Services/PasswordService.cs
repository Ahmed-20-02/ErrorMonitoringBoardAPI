namespace DevelopmentProjectErrorBoardAPI.Services
{
    using BCrypt.Net;

    public class PasswordService
    {
        public static string HashPassword(string password)
        {
            return BCrypt.HashPassword(password);
        }

        // Verify a password against a stored hashed password
        public static bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Verify(password, hash);
        }
    }
}