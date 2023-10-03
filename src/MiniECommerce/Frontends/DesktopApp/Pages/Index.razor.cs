using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using ProductService.Library.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DesktopApp.Pages
{
    public partial class Index : ComponentBase
    {
        private IEnumerable<ProductView> _products;

        protected override async Task OnInitializedAsync()
        {
            //var token = await httpContextAccessor.HttpContext
            //    .GetTokenAsync(CookieAuthenticationDefaults.AuthenticationScheme, "access_token");

            var httpClient = new HttpClient();
            var req = new HttpRequestMessage()
            {
                RequestUri = new Uri("http://gateway/api/product/product")
            };

            req.Headers.Accept.Add(new("application/json"));

            var httpResponse = await httpClient.SendAsync(req);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();

            _products = JsonSerializer.Deserialize<IEnumerable<ProductView>>(jsonResponse, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }
    }
}
