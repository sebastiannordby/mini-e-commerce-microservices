using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
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
    internal class ProductRepository : HttpRepository, IProductRepository
    {
        public ProductRepository(
            HttpClient httpClient,
            IHttpContextAccessor accessor) : base(httpClient, accessor)
        {

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

            return await Send<Guid>(req);
        }

        public async Task<ProductDto?> Find(Guid id)
        {
            var req = new HttpRequestMessage()
            {
                RequestUri = new Uri(
                    $"http://gateway/api/product-service/product/id/{id}")
            };

            return await Send<ProductDto>(req);
        }

        public async Task<IEnumerable<ProductView>?> List()
        {
            var req = new HttpRequestMessage()
            {
                RequestUri = new Uri("http://gateway/api/product-service/productview")
            };

            return await Send<IEnumerable<ProductView>>(req);
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

            await Send(req);
        }
    }
}
