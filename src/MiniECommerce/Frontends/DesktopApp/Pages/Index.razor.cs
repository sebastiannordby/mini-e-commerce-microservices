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
using OrderService.Library.Models;
using static MudBlazor.CategoryTypes;
using ComponentBase = Microsoft.AspNetCore.Components.ComponentBase;

namespace DesktopApp.Pages
{
    public partial class Index : ComponentBase
    {
        private IEnumerable<ProductView> _products = Enumerable.Empty<ProductView>();
        private IEnumerable<BasketItemView> _basketItems = Enumerable.Empty<BasketItemView>();

        private IEnumerable<IGrouping<string, ProductView>> _productGrouping => 
            _products.GroupBy(x => x.Category);
        
        private decimal? _fromPricePerQuantity;
        private decimal? _toPricePerQuantity;
        private IEnumerable<string>? _categories;
        private string? _searchValue;

        private OrderView _currentOrder;
        private bool _initialized;

        private string UserEmail =>
            HttpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value ?? "";

        protected override async Task OnInitializedAsync()
        {
            if (await TryFetchStartedOrder())
                return;

            await FetchProducts();
            await FetchBasket();
            _initialized = true;
        }

        private async Task<bool> TryFetchStartedOrder()
        {
            var orderId = await OrderRepository.GetStartedOrder();
            if (!orderId.HasValue)
                return false;

            NavigationManager.NavigateTo("/order");

            return true;
        }

        private async Task FetchProducts()
        {
            _products = await ProductRepository.List(
                _fromPricePerQuantity, _toPricePerQuantity, _categories) ?? Enumerable.Empty<ProductView>();
        }

        private async Task FetchBasket()
        {
            _basketItems = await BasketRepository.GetBasket() ?? Enumerable.Empty<BasketItemView>();
        }

        private async Task AddToBasket(ProductView product)
        {
            await BasketRepository
                .AddToBasket(product.Id);
            Snackbar.Add($"{product.Name} added to basket.");
            NavigationManager.NavigateTo("/basket");
        }

        [Inject] public required IJSRuntime JSRuntime { get; set; }
        [Inject] public required IHttpContextAccessor HttpContextAccessor { get; set; }
        [Inject] public required IOrderRepository OrderRepository { get; set; }
        [Inject] public required IProductRepository ProductRepository { get; set; }
        [Inject] public required ISnackbar Snackbar { get; set; }
        [Inject] public required IBasketRepository BasketRepository { get; set; }
        [Inject] public required NavigationManager NavigationManager { get; set; }
    }
}
