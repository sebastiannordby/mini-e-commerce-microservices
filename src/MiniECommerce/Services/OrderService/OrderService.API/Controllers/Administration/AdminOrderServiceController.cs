using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OrderService.API.Controllers.Administration
{
    [ApiController]
    [Route("api/admin/[controller]")]
    [Authorize]
    public class AdminOrderServiceController : ControllerBase
    {

    }
}
