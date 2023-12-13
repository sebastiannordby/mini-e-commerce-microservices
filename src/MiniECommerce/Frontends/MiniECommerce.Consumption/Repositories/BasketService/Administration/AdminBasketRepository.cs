using BasketService.Library;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Consumption.Repositories.BasketService.Administration
{
    internal class AdminBasketRepository : HttpRepository, IAdminBasketRepository
    {
        public AdminBasketRepository(
            HttpClient httpClient,
            IHttpContextAccessor accessor) : base(httpClient, accessor)
        {

        }

        public async Task<List<BasketItemView>> AddToBasket(string userEmail, Guid productId)
        {
            var req = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(
                    $"http://gateway/api/basket-service/adminbasket/add/{userEmail}/{productId}")
            };

            return await Send<List<BasketItemView>>(req) ?? new List<BasketItemView>();
        }

        public async Task<List<BasketItemView>> DecreaseQuantity(string userEmail, Guid productId)
        {
            var req = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(
                    $"http://gateway/api/basket-service/adminbasket/decrease-quantity/{userEmail}/{productId}")
            };

            return await Send<List<BasketItemView>>(req) ?? new List<BasketItemView>();
        }

        public async Task<IEnumerable<BasketItemView>> Get(string userEmail)
        {
            var req = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(
                    $"http://gateway/api/basket-service/adminbasket/user/{userEmail}")
            };

            return await Send<List<BasketItemView>>(req) ?? Enumerable.Empty<BasketItemView>();
        }

        public async Task<IEnumerable<string>> GetUsersBaskets()
        {
            var req = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(
                    $"http://gateway/api/basket-service/adminbasket")
            };

            return await Send<IEnumerable<string>>(req) ?? Enumerable.Empty<string>();
        }

        public async Task<List<BasketItemView>> IncreaseQuantity(string userEmail, Guid productId)
        {
            var req = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(
                    $"http://gateway/api/basket-service/adminbasket/increase-quantity/{userEmail}/{productId}")
            };

            return await Send<List<BasketItemView>>(req) ?? new List<BasketItemView>();
        }
    }
}
