using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Services.Identity.Models;

namespace Services.Identity.Managers
{
    public class PasswordManager : IPasswordManager
    {
        private const int IterationCount = 10;
        private const int BytesRequested = 256 / 8;
        private const KeyDerivationPrf Algorithm = KeyDerivationPrf.HMACSHA256;

        public IPassword EncryptPassword(string password)
        {
            var salt = new byte[128 / 8];

            using (var random = RandomNumberGenerator.Create())
                random.GetBytes(salt);

            var hash = KeyDerivation.Pbkdf2(password, salt, Algorithm, IterationCount, BytesRequested);

            return new Password(hash, salt);
        }

        public bool IsPasswordCorrect(string password, byte[] hash, byte[] salt)
        {
            var checkedHash = KeyDerivation.Pbkdf2(password, salt, Algorithm, IterationCount, BytesRequested);
            return Convert.ToBase64String(hash) == Convert.ToBase64String(checkedHash);
        }

    }
}
