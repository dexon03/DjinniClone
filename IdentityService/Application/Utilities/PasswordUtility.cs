using System.Security.Cryptography;

namespace IdentityService.Application.Utilities;

public static class PasswordUtility
{
    public static string GetHashedPassword(string password, string salt)
    {
        // Combine the password and salt
        string saltedPassword = password + salt;

        // Implementation for hashing the salted password (You can use a secure hashing algorithm)
        // For example, using SHA256
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(saltedPassword));
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }

    public static string CreatePasswordSalt()
    {
        // Generate a random salt for password hashing
        byte[] saltBytes = new byte[16];
        saltBytes = RandomNumberGenerator.GetBytes(16);

        return Convert.ToBase64String(saltBytes);
    }
}