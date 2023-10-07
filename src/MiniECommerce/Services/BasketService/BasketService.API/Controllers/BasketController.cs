using Microsoft.AspNetCore.Mvc;
using MiniECommerce.Gateway.Consumption.ProductService;
using ProductService.Library.Models;
using MiniECommece.APIUtilities;

namespace BasketService.API.Controllers
{
    public class BasketController : BasketServiceController
    {
        private readonly IGatewayProductRepository _productRepository;

        public BasketController(
            IGatewayProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpPost("productid/{productId}")]
        public async Task Test([FromRoute] Guid productId)
        {
            var product = await _productRepository
                .Find(productId, Request.GetRequestId());
            if (product == null)
                throw new Exception("Product not found");
        }
    }
}
