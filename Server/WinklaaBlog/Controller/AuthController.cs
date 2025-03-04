using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
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
            var existingUser = _dataContext.LoadDataSingle<string>(sqlCheckIfEmailExist);
            if (existingUser.Any())
                return BadRequest("Email already exists");

            var passwordSalt = _authHelpers.GenerateSalt();
            var passwordHash = _authHelpers.GetPasswordHash(userForRegistration.Password, passwordSalt);

            var sqlAddAuth = $@"INSERT INTO Auth (Email, PasswordHash, PasswordSalt)
                               VALUES ('{userForRegistration.Email}', @PasswordHash, @PasswordHash)";

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            var passSaltParam = new SqlParameter("@PasswordSalt", SqlDbType.VarBinary);
            passSaltParam.Value = passwordSalt;
            sqlParameters.Add(passSaltParam);

            var PassHashParam = new SqlParameter("@PasswordHash", SqlDbType.VarBinary);
            PassHashParam.Value = passwordHash;
            sqlParameters.Add(PassHashParam);

            if (!_dataContext.ExecuteSqlWithParameters(sqlAddAuth, sqlParameters))
                return BadRequest("Adding User Failed");

            var sqlAddUser = $@"INSERT INTO Users (Username, Email, Bio, AvatarUrl, CreateAt)
                         VALUES ( 
                            '{userForRegistration.Username}', 
                            '{userForRegistration.Email}', 
                            '{userForRegistration.Bio}', 
                            '{userForRegistration.AvatarUrl}',
                            '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')";

            if(_dataContext.ExecuteSql(sqlAddUser))
                return Ok();

            throw new Exception("Adding User Failed");
        }
    }
}
