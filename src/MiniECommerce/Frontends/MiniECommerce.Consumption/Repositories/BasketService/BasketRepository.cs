using ProductService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniECommerce.Consumption.Repositories.BasketService
{
    internal class BasketRepository : IBasketRepository
    {
        private readonly HttpClient _httpClient;

        public BasketRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Test(Guid productId)
        {
            var req = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(
                    $"http://gateway/api/basket-service/basket/productid/{productId}")
            };

            req.Headers.Accept.Add(new("application/json"));
            req.Headers.Add("RequestId", Guid.NewGuid().ToString());

            var httpResponse = await _httpClient.SendAsync(req);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
        }
    }
}
