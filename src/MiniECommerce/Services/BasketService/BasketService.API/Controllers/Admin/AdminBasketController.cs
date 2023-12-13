using BasketService.Domain.Services;
using BasketService.Library;
using Microsoft.AspNetCore.Mvc;
using MiniECommerce.Authentication.Services;
using MiniECommerce.Library.Services.ProductService;

namespace BasketService.API.Controllers.Admin
{
    public class AdminBasketController : BasketServiceController
    {
        private readonly IUserBasketService _basketService;

        public AdminBasketController(IUserBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> GetBaskets()
        {
            var result = await _basketService
                .GetUsersWithBasket();

            return result;
        }


        [HttpGet("user/{userEmail}")]
        public async Task<IEnumerable<BasketItemView>> GetList(
            [FromRoute] string userEmail)
        {
            var result = await _basketService
                .GetBasket(userEmail);

            return result;
        }

        [HttpPost("add/{userEmail}/{productId}")]
        public async Task<List<BasketItemView>> AddToBasket(
            [FromRoute] string userEmail,
            [FromRoute] Guid productId)
        {
            var result = await _basketService.AddToBasket(
                userEmail, productId);

            return result;
        }

        [HttpPost("increase-quantity/{userEmail}/{productId}")]
        public async Task<List<BasketItemView>> IncreaseQuantity(
            [FromRoute] string userEmail,
            [FromRoute] Guid productId)
        {
            var result = await _basketService.IncreaseQuantity(
                userEmail, productId);

            return result;
        }

        [HttpPost("decrease-quantity/{userEmail}/{productId}")]
        public async Task<List<BasketItemView>> DecreaseQuantity(
            [FromRoute] string userEmail,
            [FromRoute] Guid productId)
        {
            var result = await _basketService.DecreaseQuantity(
                userEmail, productId);

            return result;
        }
    }
}
