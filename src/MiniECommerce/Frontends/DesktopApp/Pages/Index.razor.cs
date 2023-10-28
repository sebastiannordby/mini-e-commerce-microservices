using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using ProductService.Library.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Authentication.Google;
using MiniECommerce.Consumption.Repositories.ProductService;
using MiniECommerce.Consumption.Repositories.BasketService;
using BasketService.Library;
using MiniECommerce.Consumption.Repositories.OrderService;
using OrderService.Library.Commands;
using System.Security.Claims;
using Microsoft.JSInterop;
using MudBlazor;
using static MudBlazor.CategoryTypes;

namespace DesktopApp.Pages
{
    public partial class Index : Microsoft.AspNetCore.Components.ComponentBase
    {
        private IEnumerable<ProductView> _products = Enumerable.Empty<ProductView>();
        private decimal? _fromPricePerQuantity;
        private decimal? _toPricePerQuantity;
        private IEnumerable<string>? _categories;
        private string? _searchValue;

        private List<BasketItemView> _basketItems = new();
     
        private string UserEmail =>
            HttpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value ?? "";


        protected override async Task OnInitializedAsync()
        {
            await FetchProducts();
            await FetchBasket();
        }

        private async Task FetchBasket()
        {
            _basketItems = await BasketRepository.GetBasket();
        }

        private async Task FetchProducts()
        {
            _products = await ProductRepository.List(
                _fromPricePerQuantity, _toPricePerQuantity, _categories) ?? new List<ProductView>();
        }

        private async Task AddToBasket(ProductView product)
        {
            _basketItems = await BasketRepository
                .AddToBasket(product.Id);
            Snackbar.Add($"{product.Name} added to basket.");
        }

        private async Task IncreaseQuantity(BasketItemView item)
        {
            _basketItems = await BasketRepository
                .IncreaseQuantity(item.ProductId);
            Snackbar.Add($"{item.ProductName} increased quantity.");
        }

        private async Task DecreaseQuantity(BasketItemView item)
        {
            _basketItems = await BasketRepository
                .DecreaseQuantity(item.ProductId);
            Snackbar.Add($"{item.ProductName} decreased quantity.");
        }

        private async Task StartOrder()
        {
            var fullName = await JSRuntime.InvokeAsync<string>("prompt", "Your full name");
            if (string.IsNullOrWhiteSpace(fullName))
                return;

            var orderId = await OrderRepository.Start(new StartOrderCommandDto()
            {
                BuyersEmailAddress = UserEmail,
                BuyersFullName = fullName 
            });

            Snackbar.Add("Order started.");
        }

        [Inject] public required IJSRuntime JSRuntime { get; set; }
        [Inject] public required IHttpContextAccessor HttpContextAccessor { get; set; }
        [Inject] public required IOrderRepository OrderRepository { get; set; }
        [Inject] public required IBasketRepository BasketRepository { get; set; }
        [Inject] public required IProductRepository ProductRepository { get; set; }
        [Inject] public required ISnackbar Snackbar { get; set; }
    }
}
