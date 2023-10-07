using ProductService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniECommerce.Consumption.Repositories.ProductService
{
    internal class ProductRepository : IProductRepository
    {
        private readonly HttpClient _httpClient;

        public ProductRepository(
            HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<Guid> Add(ProductDto product)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductView>> List()
        {
            var req = new HttpRequestMessage()
            {
                RequestUri = new Uri("http://gateway/api/product-service/product")
            };

            //var accessToken = await httpContextAccessor.HttpContext.GetTokenAsync(
            //    GoogleDefaults.AuthenticationScheme, "access_token");
            //req.Headers.Add("access_token", accessToken);

            req.Headers.Accept.Add(new("application/json"));
            req.Headers.Add("RequestId", Guid.NewGuid().ToString());

            var httpResponse = await _httpClient.SendAsync(req);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<IEnumerable<ProductView>>(jsonResponse, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        public Task Update(ProductDto product)
        {
            throw new NotImplementedException();
        }
    }
}
