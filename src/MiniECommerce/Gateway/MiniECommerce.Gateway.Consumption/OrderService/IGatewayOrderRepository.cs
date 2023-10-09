using OrderService.Library.Commands;
using ProductService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniECommerce.Gateway.Consumption.OrderService
{
    public interface IGatewayOrderRepository
    {
        Task<Guid> StartOrder(Guid requestId, StartOrderCommandDto command);
    }

    internal sealed class GatewayOrderRepository : IGatewayOrderRepository
    {
        private readonly HttpClient _httpClient;

        public GatewayOrderRepository(
            HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Guid> StartOrder(Guid requestId, StartOrderCommandDto command)
        {
            var req = new HttpRequestMessage()
            {
                RequestUri = new Uri(
                $"http://gateway/api/order-service/order/place")
            };

            req.Headers.Accept.Add(new("application/json"));
            req.Headers.Add("RequestId", requestId.ToString());

            var httpResponse = await _httpClient.SendAsync(req);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Guid>(jsonResponse, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }
    }
}
