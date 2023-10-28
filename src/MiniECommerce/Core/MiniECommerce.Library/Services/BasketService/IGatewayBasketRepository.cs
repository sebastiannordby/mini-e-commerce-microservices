using BasketService.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniECommerce.Library.Services.BasketService
{
    public interface IGatewayBasketRepository
    {
        Task<IEnumerable<BasketItemView>?> GetList(string userEmail);
    }

    internal sealed class GatewayBasketRepository : IGatewayBasketRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AuthorizationHeaderService _authHeaderService;

        public GatewayBasketRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<BasketItemView>?> GetList(string userEmail)
        {
            var req = new HttpRequestMessage()
            {
                RequestUri = new Uri(
                $"http://gateway/api/basket-service/basket")
            };

            req.Headers.Accept.Add(new("application/json"));

            var httpResponse = await _httpClientFactory
                .CreateClient("GatewayClient")
                .SendAsync(req);
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
