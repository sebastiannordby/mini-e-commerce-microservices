using Microsoft.AspNetCore.Components.Authorization;
using ProductService.Library.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MiniECommerce.Consumption.Repositories.ProductService
{
    internal class ProductRepository : IProductRepository
    {
        private readonly HttpClient _httpClient;

        public ProductRepository(
            AuthenticationStateProvider prov,
            HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Guid> Add(ProductDto product)
        {
            var req = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("http://gateway/api/product-service/product"),
                Content = new StringContent(
                    JsonSerializer.Serialize(product), Encoding.UTF8, "application/json")
            };

            var requestId = Guid.NewGuid().ToString();

            Log.Information(
                $"Sending request({requestId}) to: {req.RequestUri}");

            req.Headers.Accept.Add(new("application/json"));
            req.Headers.Add("RequestId", requestId);

            var httpResponse = await _httpClient.SendAsync(req);

            Log.Information(
                $"Request({requestId}) Statuscode: {httpResponse.StatusCode} to: {req.RequestUri}");

            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Guid>(jsonResponse, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        public async Task<ProductDto> Find(Guid id)
        {
            var req = new HttpRequestMessage()
            {
                RequestUri = new Uri(
                    $"http://gateway/api/product-service/product/id/{id}")
            };

            req.Headers.Accept.Add(new("application/json"));
            req.Headers.Add("RequestId", Guid.NewGuid().ToString());

            var httpResponse = await _httpClient.SendAsync(req);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ProductDto>(jsonResponse, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        public async Task<IEnumerable<ProductView>> List()
        {
            var req = new HttpRequestMessage()
            {
                RequestUri = new Uri("http://gateway/api/product-service/productview")
            };

            req.Headers.Accept.Add(new("application/json"));
            req.Headers.Add("RequestId", Guid.NewGuid().ToString());

            var httpResponse = await _httpClient.SendAsync(req);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<IEnumerable<ProductView>>(jsonResponse, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        public async Task Update(ProductDto product)
        {
            var req = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri("http://gateway/api/product-service/product"),
                Content = new StringContent(
                    JsonSerializer.Serialize(product), Encoding.UTF8, "application/json")
            };

            var requestId = Guid.NewGuid().ToString();

            Log.Information(
                $"Sending request({requestId}) to: {req.RequestUri}");

            req.Headers.Accept.Add(new("application/json"));
            req.Headers.Add("RequestId", requestId);

            var httpResponse = await _httpClient.SendAsync(req);

            Log.Information(
                $"Request({requestId}) Statuscode: {httpResponse.StatusCode} to: {req.RequestUri}");
        }
    }
}
