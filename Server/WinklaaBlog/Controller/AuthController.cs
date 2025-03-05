using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Data;
using System.Security.Cryptography;
using WinklaaBlog.Data;
using WinklaaBlog.DTO;
using WinklaaBlog.Helpers;
using WinklaaBlog.Models;

namespace WinklaaBlog.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDataContext _dataContext;
        private readonly AuthHelpers _authHelpers;
        public AuthController(IConfiguration config)
        {
            _dataContext = new(config);
            _authHelpers = new(config);
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register(UserForRegistrationDto userForRegistration)
        {
            if (userForRegistration == null)
                return BadRequest("User is null");

            if (userForRegistration.Password != userForRegistration.PasswordConfirm)
                return BadRequest("Passwords do not match");

            var sqlCheckIfEmailExist = $"SELECT Email FROM Auth WHERE Email = '{userForRegistration.Email}'";
            var existingUser = _dataContext.LoadDataOrDefaultSingle<string>(sqlCheckIfEmailExist);
            if (existingUser != null)
                return BadRequest("Email already exists");

            var passwordSalt = _authHelpers.GenerateSalt();
            var passwordHash = _authHelpers.GetPasswordHash(userForRegistration.Password, passwordSalt);

            var sqlAddAuth = $@"INSERT INTO Auth (Email, PasswordHash, PasswordSalt)
                               VALUES ('{userForRegistration.Email}', @PasswordHash, @PasswordSalt)";

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            var passSaltParam = new SqlParameter("@PasswordSalt", SqlDbType.VarBinary);
            passSaltParam.Value = passwordSalt;
            sqlParameters.Add(passSaltParam);

            var PassHashParam = new SqlParameter("@PasswordHash", SqlDbType.VarBinary);
            PassHashParam.Value = passwordHash;
            sqlParameters.Add(PassHashParam);

            if (!_dataContext.ExecuteSqlWithParameters(sqlAddAuth, sqlParameters))
                return BadRequest("Adding User Failed");

            var sqlAddUser = $@"INSERT INTO Users (Username, Email, Bio, AvatarUrl, CreatedAt)
                         VALUES ( 
                            '{userForRegistration.Username}', 
                            '{userForRegistration.Email}', 
                            '{userForRegistration.Bio}', 
                            '{userForRegistration.AvatarUrl}',
                            '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')";

            if (_dataContext.ExecuteSql(sqlAddUser))
                return Ok();

            throw new Exception("Adding User Failed");
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login(UserForLogin userForLogin)
        {
            var sql = $"SELECT PasswordHash, PasswordSalt FROM Auth WHERE Email = '{userForLogin.Email}'";
            var userForLoginConfirmation = new UserForLoginConfirmationDto();

            try
            {
                userForLoginConfirmation = _dataContext.LoadDataSingle<UserForLoginConfirmationDto>(sql);
            }
            catch (Exception e)
            {
                return StatusCode(401, e);
            }

            var passwordHash = _authHelpers.GetPasswordHash(userForLogin.Password, userForLoginConfirmation.PasswordSalt);

            for (int i = 0; i < passwordHash.Length; i++)
            {
                if (passwordHash[i] != userForLoginConfirmation.PasswordHash[i])
                {
                    return StatusCode(401, "Incorrect password");
                }
            }

            var getUserIdSql = $"SELECT Id FROM Users WHERE Email = '{userForLogin.Email}'";
            var userId = _dataContext.LoadDataSingle<int>(getUserIdSql);

            return Ok(new Dictionary<string, string>
            {
                {"token", _authHelpers.CreateToken(userId) }
            });
        }

        [HttpGet("RefreshToken")]
        public IActionResult RefreshToken()
        {
            var userIdSql = $@"SELECT Id FROM Users WHERE Id = {User.FindFirst("userId")?.Value + ""}";
            var userId = _dataContext.LoadDataSingle<int>(userIdSql);

            return Ok(new Dictionary<string, string>
            {
                {"token", _authHelpers.CreateToken(userId) }
            });
        }
    }
}
