using System.Text;
using System.Security.Cryptography;

namespace API_TimeTracker.Utilities
{
    public class PasswordUtility
    {
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public static bool VerifyPassword(string inputPassword, string storedHashedPassword)
        {
            string hashedInput = HashPassword(inputPassword);
            return storedHashedPassword.Equals(hashedInput);
        }
    }
}
