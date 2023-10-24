using ProductService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniECommerce.Gateway.Consumption.ProductService
{
    public interface IGatewayProductRepository
    {
        Task<ProductView?> Find(Guid id, Guid requestId);
    }

    internal sealed class GatewayProductRepository : IGatewayProductRepository
    {
        private readonly HttpClient _httpClient;
        private readonly AuthorizationHeaderService _authHeaderService;

        public GatewayProductRepository(
            HttpClient httpClient,
            AuthorizationHeaderService authHeaderService)
        {
            _httpClient = httpClient;
            _authHeaderService = authHeaderService;
        }

        public async Task<ProductView?> Find(
            Guid id, Guid requestId)
        {
            var req = new HttpRequestMessage()
            {
                RequestUri = new Uri(
                    $"http://gateway/api/product-service/productview/id/{id}")
            };

            req.Headers.Accept.Add(new("application/json"));
            req.Headers.Add("RequestId", requestId.ToString());
            req.Headers.Add("Authorization", 
                await _authHeaderService.GetAuthorizationHeaderValue());

            var httpResponse = await _httpClient.SendAsync(req);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ProductView>(jsonResponse, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }
    }
}
