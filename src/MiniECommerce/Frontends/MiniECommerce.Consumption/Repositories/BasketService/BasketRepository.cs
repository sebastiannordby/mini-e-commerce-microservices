using BasketService.Library;
using Microsoft.AspNetCore.Http;
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
        public BasketRepository(
            HttpClient httpClient,
            IHttpContextAccessor httpContextAccessor
        ) : base(httpClient, httpContextAccessor)
        {

        }

        public async Task<List<BasketItemView>> AddToBasket(Guid productId)
        {
            var req = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(
                    $"http://gateway/api/basket-service/basket/add/productid/{productId}")
            };

            return await Send<List<BasketItemView>>(req) ?? new List<BasketItemView>();
        }

        public async Task<List<BasketItemView>> DecreaseQuantity(Guid productId)
        {
            var req = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(
                    $"http://gateway/api/basket-service/basket/decrease-quantity/{productId}")
            };

            return await Send<List<BasketItemView>>(req) ?? new List<BasketItemView>();
        }

        public async Task<List<BasketItemView>> IncreaseQuantity(Guid productId)
        {
            var req = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(
                    $"http://gateway/api/basket-service/basket/increase-quantity/{productId}")
            };

            return await Send<List<BasketItemView>>(req) ?? new List<BasketItemView>();
        }
    }
}
