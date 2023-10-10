using Microsoft.AspNetCore.Mvc;
using MiniECommerce.Gateway.Consumption.ProductService;
using ProductService.Library.Models;
using MiniECommece.APIUtilities;
using BasketService.Library;
using System.Threading.Tasks;
using System;
using System.Collections.Concurrent;

namespace BasketService.API.Controllers
{
    public class BasketController : BasketServiceController
    {
        private readonly IGatewayProductRepository _productRepository;
        private static readonly ConcurrentDictionary<string, List<BasketItemView>> _baskets = new();
        
        public BasketController(
            IGatewayProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("{userEmail}")]
        public IEnumerable<BasketItemView> GetList([FromRoute] string userEmail)
        {
            var basket = _baskets
                .FirstOrDefault(x => x.Key == userEmail);

            return basket.Value ?? new();
        }

        [HttpPost("add/{userEmail}/productid/{productId}")]
        public async Task<List<BasketItemView>> AddToBasket(
            [FromRoute] string userEmail,
            [FromRoute] Guid productId)
        {
            var product = await _productRepository
                .Find(productId, Request.GetRequestId());
            if (product == null)
                throw new Exception("Product not found");

            var newBasketItem = new BasketItemView()
            {
                PricePerQuantity = product.PricePerQuantity,
                ProductId = productId,
                ProductName = product.Name,
                Quantity = 1
            };

            var basketItems = _baskets.AddOrUpdate(userEmail,
                addValue: new List<BasketItemView>() { newBasketItem }, (key, value) =>
                {
                    if (value.Any(x => x.ProductId == productId))
                        throw new Exception("Product already exists in basket.");

                    value.Add(newBasketItem);

                    return value;
                });

            return basketItems;
        }

        [HttpPost("increase-quantity/{userEmail}/{productId}")]
        public List<BasketItemView> IncreaseQuantity(
            [FromRoute] string userEmail,
            [FromRoute] Guid productId)
        {
            if (!_baskets.TryGetValue(userEmail, out var basketItems))
                throw new ArgumentException("Could not find basket.");

            var basketItem = basketItems
                .FirstOrDefault(x => x.ProductId == productId);
            if (basketItem == null)
                throw new Exception("Product not in basket.");

            basketItem.Quantity += 1;

            return basketItems;
        }

        [HttpPost("decrease-quantity/{userEmail}/{productId}")]
        public List<BasketItemView> DecreaseQuantity(
            [FromRoute] string userEmail,
            [FromRoute] Guid productId)
        {
            if (!_baskets.TryGetValue(userEmail, out var basketItems))
                throw new ArgumentException("Could not find basket.");

            var basketItem = basketItems
                .FirstOrDefault(x => x.ProductId == productId);
            if (basketItem == null)
                throw new Exception("Product not in basket.");

            basketItem.Quantity -= 1;

            if (basketItem.Quantity <= 0)
                basketItems.Remove(basketItem);

            return basketItems;
        }
    }
}
