using Microsoft.AspNetCore.Mvc;
using MiniECommerce.Authentication.Services;
using UserService.DataAccess.Repositories;

namespace UserService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserInfoViewRepository _repository;

        public AdminController(
            ILogger<UserController> logger,
            IUserInfoViewRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult?> Get(
            [FromRoute] string email)
        {
            _logger.LogInformation("Fetching user {0}", email);

            var result = await _repository.Get(email);

            return Ok(result);
        }
    }
}
