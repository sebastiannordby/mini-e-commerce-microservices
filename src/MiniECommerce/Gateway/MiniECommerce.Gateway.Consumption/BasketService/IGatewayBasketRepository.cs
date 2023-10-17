using BasketService.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniECommerce.Gateway.Consumption.BasketService
{
    public interface IGatewayBasketRepository
    {
        Task<IEnumerable<BasketItemView>?> GetList(Guid requestId, string userEmail);
    }

    internal sealed class GatewayBasketRepository : IGatewayBasketRepository
    {
        private readonly HttpClient _httpClient;

        public GatewayBasketRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<BasketItemView>?> GetList(Guid requestId, string userEmail)
        {
            var req = new HttpRequestMessage()
            {
                RequestUri = new Uri(
                $"http://gateway/api/basket-service/basket")
            };

            req.Headers.Accept.Add(new("application/json"));
            req.Headers.Add("RequestId", requestId.ToString());

            var httpResponse = await _httpClient.SendAsync(req);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(jsonResponse))
                return null;

            return JsonSerializer.Deserialize<IEnumerable<BasketItemView>>(jsonResponse, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }
    }
}
