using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace WinklaaBlog.Helpers
{
    public class AuthHelpers
    {
        private IConfiguration _configuration;

        public AuthHelpers(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public byte[] GetPasswordHash(string password, byte[] passwordSalt)
        {
            var passwordSaltString = _configuration.GetSection("AppSettings:PasswordKey").Value + Convert.ToBase64String(passwordSalt);
            var hash = KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.UTF8.GetBytes(passwordSaltString),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 1000000,
                numBytesRequested: 256 / 8
            );

            return hash;
        }
    }
}
