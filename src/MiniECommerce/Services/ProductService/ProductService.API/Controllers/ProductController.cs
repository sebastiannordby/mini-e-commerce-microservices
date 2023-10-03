using Microsoft.AspNetCore.Mvc;

namespace ProductService.API.Controllers
{
    public class ProductController : ProductServiceController
    {
        public IActionResult Index()
        {
            return Ok("This is the product");
        }
    }
}
