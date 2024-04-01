using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace BooksLibrary.Api.Helper;

public class PasswordHasher
{
    // This method hashes a password using the SHA256 algorithm
    public static string HashPassword(string password)
    {
        byte[] salt = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        byte[] hash = KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 32);

        byte[] hashBytes = new byte[hash.Length + salt.Length];
        Buffer.BlockCopy(hash, 0, hashBytes, 0, hash.Length);
        Buffer.BlockCopy(salt, 0, hashBytes, hash.Length, salt.Length);

        return Convert.ToBase64String(hashBytes);
    }

    // This method compares a hashed password with a plain-text password
    public static bool VerifyPassword(string hashedPassword, string providedPassword)
    {
        byte[] hashBytes = Convert.FromBase64String(hashedPassword);
        byte[] salt = new byte[16];
        Buffer.BlockCopy(hashBytes, 32, salt, 0, salt.Length);

        byte[] hashToVerify = KeyDerivation.Pbkdf2(
            password: providedPassword,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 32);

        for (int i = 0; i < 32; i++)
        {
            if (hashBytes[i] != hashToVerify[i])
            {
                return false;
            }
        }

        return true;
    }
}