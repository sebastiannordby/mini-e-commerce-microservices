using BasketService.Library;
using ProductService.Library.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniECommerce.Consumption.Repositories.BasketService
{
    internal class BasketRepository : HttpRepository, IBasketRepository
    {
        public BasketRepository(HttpClient httpClient) : base(httpClient)
        {
        
        }

        public async Task<List<BasketItemView>> AddToBasket(string userEmail, Guid productId)
        {
            var req = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(
                    $"http://gateway/api/basket-service/basket/add/{userEmail}/productid/{productId}")
            };

            return await Send<List<BasketItemView>>(req) ?? new List<BasketItemView>();
        }

        public async Task<List<BasketItemView>> DecreaseQuantity(string userEmail, Guid productId)
        {
            var req = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(
                    $"http://gateway/api/basket-service/basket/decrease-quantity/{userEmail}/{productId}")
            };

            return await Send<List<BasketItemView>>(req) ?? new List<BasketItemView>();
        }

        public async Task<List<BasketItemView>> IncreaseQuantity(string userEmail, Guid productId)
        {
            var req = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(
                    $"http://gateway/api/basket-service/basket/increase-quantity/{userEmail}/{productId}")
            };

            return await Send<List<BasketItemView>>(req) ?? new List<BasketItemView>();
        }
    }
}
