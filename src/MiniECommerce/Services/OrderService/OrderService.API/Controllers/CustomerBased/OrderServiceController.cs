using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OrderService.API.Controllers.CustomerBased
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public abstract class OrderServiceController : ControllerBase
    {

    }
}
