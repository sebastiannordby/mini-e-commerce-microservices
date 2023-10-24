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
        Task<Guid> StartOrder(Guid requestId, StartOrderCommandDto command);
    }

    internal sealed class GatewayOrderRepository : IGatewayOrderRepository
    {
        private readonly HttpClient _httpClient;
        private readonly AuthorizationHeaderService _authHeaderService;

        public GatewayOrderRepository(
            HttpClient httpClient,
            AuthorizationHeaderService authHeaderService)
        {
            _httpClient = httpClient;
            _authHeaderService = authHeaderService;
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
            req.Headers.Add("Authorization",
                await _authHeaderService.GetAuthorizationHeaderValue());

            var httpResponse = await _httpClient.SendAsync(req);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Guid>(jsonResponse, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }
    }
}
