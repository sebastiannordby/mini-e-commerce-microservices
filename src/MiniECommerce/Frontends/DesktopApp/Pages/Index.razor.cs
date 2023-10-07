using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using ProductService.Library.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Authentication.Google;

namespace DesktopApp.Pages
{
    public partial class Index : ComponentBase
    {
        private IEnumerable<ProductView> _products;

        protected override async Task OnInitializedAsync()
        {
            var req = new HttpRequestMessage()
            {
                RequestUri = new Uri("http://gateway/api/productservice/product")
            };

            var accessToken = await httpContextAccessor.HttpContext.GetTokenAsync(
                GoogleDefaults.AuthenticationScheme, "access_token");

            req.Headers.Add("access_token", accessToken);

            req.Headers.Accept.Add(new("application/json"));

            var httpResponse = await HttpClient.SendAsync(req);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();

            _products = JsonSerializer.Deserialize<IEnumerable<ProductView>>(jsonResponse, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        [Inject] private HttpClient HttpClient { get; set; }
    }
}
