using OrderService.Library.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniECommerce.Library.Services.OrderService
{
    public interface IGatewayOrderRepository
    {
        Task<Guid> StartOrder(StartOrderCommandDto command);
    }

    internal sealed class GatewayOrderRepository : IGatewayOrderRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GatewayOrderRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Guid> StartOrder(StartOrderCommandDto command)
        {
            var req = new HttpRequestMessage()
            {
                RequestUri = new Uri(
                $"http://gateway/api/order-service/order/start")
            };

            req.Headers.Accept.Add(new("application/json"));

            var httpResponse = await _httpClientFactory.CreateClient().SendAsync(req);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Guid>(jsonResponse, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }
    }
}
