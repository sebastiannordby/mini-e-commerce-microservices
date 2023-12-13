using Microsoft.AspNetCore.Mvc;
using MiniECommerce.Library.Services.ProductService;
using ProductService.Library.Models;
using MiniECommece.APIUtilities;
using BasketService.Library;
using System.Threading.Tasks;
using System;
using System.Collections.Concurrent;
using BasketService.Domain.Services;
using MiniECommerce.Authentication.Services;

namespace BasketService.API.Controllers.User
{
    public class BasketController : BasketServiceController
    {
        private readonly IUserBasketService _basketService;
        private readonly ICurrentUserService _currentUserService;

        public BasketController(
            IUserBasketService basketService,
            ICurrentUserService currentUserService)
        {
            _basketService = basketService;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<IEnumerable<BasketItemView>> GetList()
        {
            return await _basketService.GetBasket(
                _currentUserService.UserEmail);
        }

        [HttpPost("add/productid/{productId}")]
        public async Task<List<BasketItemView>> AddToBasket(
            [FromRoute] Guid productId)
        {
            return await _basketService.AddToBasket(
                _currentUserService.UserEmail, productId);
        }

        [HttpPost("increase-quantity/{productId}")]
        public async Task<List<BasketItemView>> IncreaseQuantity(
            [FromRoute] Guid productId)
        {
            return await _basketService.IncreaseQuantity(
                _currentUserService.UserEmail, productId);
        }

        [HttpPost("decrease-quantity/{productId}")]
        public async Task<List<BasketItemView>> DecreaseQuantity(
            [FromRoute] Guid productId)
        {
            return await _basketService.DecreaseQuantity(
                _currentUserService.UserEmail, productId);
        }
    }
}
