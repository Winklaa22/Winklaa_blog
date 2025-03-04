using Microsoft.AspNetCore.Mvc;
using WinklaaBlog.Data;
using WinklaaBlog.Helpers;

namespace WinklaaBlog.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController
    {
        private readonly ApplicationDataContext _dataContext;
        private readonly AuthHelpers _authHelpers;
        public AuthController(IConfiguration config)
        {
            _dataContext = new(config);
            _authHelpers = new(config);
        }



    }
}
