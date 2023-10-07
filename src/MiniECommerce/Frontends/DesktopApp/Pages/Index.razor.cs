using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using ProductService.Library.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Authentication.Google;
using MiniECommerce.Consumption.Repositories.ProductService;

namespace DesktopApp.Pages
{
    public partial class Index : ComponentBase
    {
        private IEnumerable<ProductView> _products;

        protected override async Task OnInitializedAsync()
        {
            _products = await ProductRepository.List();
        }

        [Inject] private IProductRepository ProductRepository { get; set; }
    }
}
