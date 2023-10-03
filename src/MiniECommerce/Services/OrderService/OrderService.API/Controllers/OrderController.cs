using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OrderService.API.Controllers
{
    public class OrderController : OrderServiceController
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("OrderController: Hello");
        }
    }
}