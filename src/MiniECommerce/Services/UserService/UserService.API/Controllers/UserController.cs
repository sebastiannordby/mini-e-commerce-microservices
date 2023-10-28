using Microsoft.AspNetCore.Mvc;
using UserService.Library;

namespace UserService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public UserInfoView? Get()
        {
            return null;
        }
    }
}