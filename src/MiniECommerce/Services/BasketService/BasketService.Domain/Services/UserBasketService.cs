using BasketService.Library;
using MiniECommerce.Gateway.Consumption.ProductService;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Domain.Services
{
    internal class UserBasketService : IUserBasketService
    {
        private static readonly ConcurrentDictionary<string, List<BasketItemView>> _baskets = new();
        private readonly IGatewayProductRepository _productRepository;

        public UserBasketService(
            IGatewayProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<BasketItemView>> AddToBasket(
            Guid requestId, string userEmail, Guid productId)
        {
            var product = await _productRepository
                .Find(productId, requestId);
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

        public Task ClearBasket(string userEmail)
        {
            _baskets.Remove(userEmail, out var _);

            return Task.CompletedTask;
        }

        public Task<List<BasketItemView>> DecreaseQuantity(string userEmail, Guid productId)
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

            return Task.FromResult(basketItems);
        }

        public Task<List<BasketItemView>> GetBasket(string userEmail)
        {
            var basket = _baskets
                .FirstOrDefault(x => x.Key == userEmail);

            return Task.FromResult(basket.Value ?? new());
        }

        public Task<List<BasketItemView>> IncreaseQuantity(string userEmail, Guid productId)
        {
            if (!_baskets.TryGetValue(userEmail, out var basketItems))
                throw new ArgumentException("Could not find basket.");

            var basketItem = basketItems
                .FirstOrDefault(x => x.ProductId == productId);
            if (basketItem == null)
                throw new Exception("Product not in basket.");

            basketItem.Quantity += 1;

            return Task.FromResult(basketItems);
        }
    }
}
