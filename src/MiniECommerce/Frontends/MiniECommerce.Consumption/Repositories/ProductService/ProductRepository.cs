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
using System.Web;

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

        public async Task<IEnumerable<ProductView>?> List(
            string? searchValue,
            decimal? fromPricePerQuantity = null,
            decimal? toPricePerQuantity = null,
            IEnumerable<string>? categories = null)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            if (!string.IsNullOrWhiteSpace(searchValue))
                queryString["search"] = searchValue;

            if (fromPricePerQuantity.HasValue)
                queryString["fromPricePerQuantity"] = fromPricePerQuantity.Value.ToString();

            if (toPricePerQuantity.HasValue)
                queryString["toPricePerQuantity"] = toPricePerQuantity.Value.ToString();

            foreach (var category in categories ?? Enumerable.Empty<string>())
                queryString.Add("categories", category);

            var uriBuilder = new UriBuilder("http://gateway/api/product-service/productview")
            {
                Query = queryString.ToString()
            };

            var req = new HttpRequestMessage()
            {
                RequestUri = uriBuilder.Uri
            };

            return await Send<IEnumerable<ProductView>>(req);
        }

        public async Task<IEnumerable<string>> ListCategories()
        {
            var req = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("http://gateway/api/product-service/product/categories"),
            };

            return await Send<IEnumerable<string>>(req) ?? Enumerable.Empty<string>();
        }

        public async Task<IEnumerable<ProductView>> TopTen()
        {
            var req = new HttpRequestMessage()
            {
                RequestUri = new Uri(
                    $"http://gateway/api/product-service/productview/top-ten")
            };

            return await Send<IEnumerable<ProductView>>(req) ?? Enumerable.Empty<ProductView>();
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
