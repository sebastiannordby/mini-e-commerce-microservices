using Microsoft.AspNetCore.Mvc;
using MiniECommerce.Gateway.Consumption.ProductService;
using ProductService.Library.Models;
using MiniECommece.APIUtilities;
using BasketService.Library;
using System.Threading.Tasks;
using System;
using System.Collections.Concurrent;
using BasketService.Domain.Services;

namespace BasketService.API.Controllers
{
    public class BasketController : BasketServiceController
    {
        private readonly IUserBasketService _basketService;
        private readonly IGatewayProductRepository _productRepository;
        
        public BasketController(
            IUserBasketService basketService,
            IGatewayProductRepository productRepository)
        {
            _basketService = basketService;
            _productRepository = productRepository;
        }

        [HttpGet("{userEmail}")]
        public async Task<IEnumerable<BasketItemView>> GetList(
            [FromRoute] string userEmail)
        {
            return await _basketService.GetBasket(userEmail);
        }

        [HttpPost("add/{userEmail}/productid/{productId}")]
        public async Task<List<BasketItemView>> AddToBasket(
            [FromRoute] string userEmail,
            [FromRoute] Guid productId)
        {
            return await _basketService.AddToBasket(
                Request.GetRequestId(), userEmail, productId);
        }

        [HttpPost("increase-quantity/{userEmail}/{productId}")]
        public async Task<List<BasketItemView>> IncreaseQuantity(
            [FromRoute] string userEmail,
            [FromRoute] Guid productId)
        {
            return await _basketService.IncreaseQuantity(userEmail, productId);
        }

        [HttpPost("decrease-quantity/{userEmail}/{productId}")]
        public async Task<List<BasketItemView>> DecreaseQuantity(
            [FromRoute] string userEmail,
            [FromRoute] Guid productId)
        {
            return await _basketService.DecreaseQuantity(userEmail, productId);
        }
    }
}
