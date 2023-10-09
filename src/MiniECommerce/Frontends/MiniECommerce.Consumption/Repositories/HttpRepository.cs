using ProductService.Library.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniECommerce.Consumption.Repositories
{
    internal class HttpRepository
    {
        protected readonly HttpClient _httpClient;

        public HttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T?> Send<T>(HttpRequestMessage req)
        {
            var httpResponse = await Send(req);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(jsonResponse, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        public async Task<HttpResponseMessage> Send(HttpRequestMessage req)
        {
            var requestId = Guid.NewGuid().ToString();

            Log.Information(
                "Request({0}): {1}", requestId, req.RequestUri);

            req.Headers.Accept.Add(new("application/json"));
            req.Headers.Add("RequestId", requestId);

            var httpResponse = await _httpClient.SendAsync(req);

            Log.Information("Request({0}): {1} - {2} ",
                requestId, req.RequestUri, httpResponse.StatusCode);

            return httpResponse;
        }
    }
}
