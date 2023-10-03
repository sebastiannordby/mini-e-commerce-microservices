using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OrderService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public abstract class OrderServiceController : ControllerBase
    {

    }
}
