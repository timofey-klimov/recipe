using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Recipes.Domain.Core.Services;
using System.Text;

namespace Recipes.Infrustructure.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Hash(string password, string salt)
        {
            if (password is null)
                throw new ArgumentNullException(nameof(password));

            if (salt is null)
                throw new ArgumentNullException(nameof(salt));

            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password!,
                salt: Encoding.UTF8.GetBytes(salt),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
        }
    }
}
