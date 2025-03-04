using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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
            return KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.ASCII.GetBytes(passwordSaltString),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 1000000,
                numBytesRequested: 256 / 8
            );
        }

        public byte[] GenerateSalt()
        {
            var passwordSalt = new byte[128 / 8];
            using RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetNonZeroBytes(passwordSalt);
            return passwordSalt;
        }

        public string CreateToken(int userId)
        {
            var caims = new Claim[]
            {
                new Claim("userId", userId.ToString())
            };

            var keyValue = _configuration.GetSection("AppSettings:TokenKey")?.Value;
            var tokenKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    keyValue != null ? keyValue : ""
                )
            );

            var credentials = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha512Signature);

            var desctiptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(caims),
                Expires = DateTime.Now.AddHours(24),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(desctiptor);
            return tokenHandler.WriteToken(securityToken);
        }
    }
}
