using Microsoft.AspNetCore.Mvc;

namespace PurchaseService.API.Controllers
{
    public class PurchaseController : PurchaseServiceController
    {
        public IActionResult Index()
        {
            return Ok("PurchaseController: Hello");
        }
    }
}
