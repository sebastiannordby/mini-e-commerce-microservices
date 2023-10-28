using ProductService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniECommerce.Library.Services.ProductService
{
    public interface IGatewayProductRepository
    {
        Task<ProductView?> Find(Guid id);
    }

    internal sealed class GatewayProductRepository : IGatewayProductRepository
    {
        private HttpClient _httpClient;

        public GatewayProductRepository(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        public async Task<ProductView?> Find(Guid id)
        {
            var req = new HttpRequestMessage()
            {
                RequestUri = new Uri(
                    $"http://gateway/api/product-service/productview/id/{id}")
            };

            req.Headers.Accept.Add(new("application/json"));

            var httpResponse = await _httpClient.SendAsync(req);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ProductView>(jsonResponse, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }
    }
}
