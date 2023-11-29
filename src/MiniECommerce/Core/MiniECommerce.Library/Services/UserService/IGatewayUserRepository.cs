using ProductService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UserService.Library;

namespace MiniECommerce.Library.Services.UserService
{
    public interface IGatewayUserRepository
    {
        Task<UserInfoView?> Find(string userEmailAddress);
    }

    internal sealed class GatewayUserRepository : IGatewayUserRepository
    {
        private readonly HttpClient _httpClient;

        public GatewayUserRepository(
            IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("GatewayClient");
        }

        public async Task<UserInfoView?> Find(string userEmailAddress)
        {
            var req = new HttpRequestMessage()
            {
                RequestUri = new Uri(
                    $"http://gateway/api/user-service/admin/email/{userEmailAddress}")
            };

            req.Headers.Accept.Add(new("application/json"));

            var httpResponse = await _httpClient.SendAsync(req);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            var result = !string.IsNullOrWhiteSpace(jsonResponse) ? JsonSerializer.Deserialize<UserInfoView?>(jsonResponse, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }) : null;

            return result;
        }
    }
}
