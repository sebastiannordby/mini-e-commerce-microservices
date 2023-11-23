using Microsoft.AspNetCore.Mvc;
using MiniECommerce.Authentication.Services;
using UserService.DataAccess.Repositories;
using UserService.Library;

namespace UserService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserInfoViewRepository _repository;
        private readonly ICurrentUserService _currentUserService;

        public UserController(
            ILogger<UserController> logger,
            IUserInfoViewRepository repository,
            ICurrentUserService currentUserService)
        {
            _logger = logger;
            _repository = repository;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<IActionResult?> Get()
        {
            var result = await _repository.Get(
                _currentUserService.UserEmail);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] UserInfoView userInfoView)
        {
            await _repository.Save(userInfoView);

            return Ok();
        }
    }
}