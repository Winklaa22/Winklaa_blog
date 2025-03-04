using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WinklaaBlog.Data;
using WinklaaBlog.DTO;
using WinklaaBlog.Models;

namespace WinklaaBlog.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDataContext _dataContext;

        public UsersController(ApplicationDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("GetSingleUser")]
        public User GetSingleUser(string userId)
        {
            return _dataContext.LoadDataSingle<User>($"SELECT * FROM Users WHERE Id = '{userId}'");
        }

        [HttpGet("GetUsers")]
        public IEnumerable<User> GetUsers()
        {
            return _dataContext.LoadData<User>("SELECT * FROM Users");
        }

        [HttpPost("CreateUser")]
        public IActionResult AddUser(UserToAdd user)
        {
            var sql = $@"INSERT INTO Users (Username, Email, PasswordHash, Bio, AvatarUrl, CreateAt)
                         VALUES ( 
                            '{user.Username}', 
                            '{user.Email}', 
                            '{user.PasswordHash}', 
                            '{user.Bio}', 
                            '{user.AvatarUrl}',
                            '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')";

            if(_dataContext.ExecuteSql(sql))
            {
                return Ok();
            }
            return BadRequest("Adding User Failed");
        }
    }
}
