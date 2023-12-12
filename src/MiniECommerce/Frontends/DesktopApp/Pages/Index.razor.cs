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
using Prometheus;
using Microsoft.AspNetCore.Components.Web;

namespace DesktopApp.Pages
{
    public partial class Index : ComponentBase, IDisposable
    {
        private IEnumerable<ProductView> _products = Enumerable.Empty<ProductView>();
        private IEnumerable<BasketItemView> _basketItems = Enumerable.Empty<BasketItemView>();
        private IEnumerable<string> _categories = Enumerable.Empty<string>();

        private IEnumerable<IGrouping<string, ProductView>> _productGrouping => 
            _products.GroupBy(x => x.Category);

        private IEnumerable<ProductView> _topTenProducts;

        public static readonly Gauge UsersBrowsingProducts = Metrics.CreateGauge(
            "users_in_catalog",
            "Active users browsing products.");

        private decimal? _fromPricePerQuantity;
        private decimal? _toPricePerQuantity;
        private IEnumerable<string>? _selectedCategories;
        private string? _searchValue;

        private OrderView _currentOrder;
        private bool _initialized;

        private string UserEmail =>
            HttpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value ?? "";

        protected override async Task OnInitializedAsync()
        {
            if (await TryFetchStartedOrder())
                return;

            await FetchTopTenProducts();
            await FetchProducts();
            await FetchBasket();
            await FetchCategories();
            _initialized = true;
            UsersBrowsingProducts.Inc(); 
        }

        private async Task FetchCategories()
        {
            _categories = await ProductRepository.ListCategories();
        }

        private async Task<bool> TryFetchStartedOrder()
        {
            var orderId = await OrderRepository.GetStartedOrder();
            if (!orderId.HasValue)
                return false;

            NavigationManager.NavigateTo("/order");

            return true;
        }

        private async Task FetchTopTenProducts()
        {
            _topTenProducts = await ProductRepository.TopTen();
        }

        private async Task FetchProducts()
        {
            _products = await ProductRepository.List(
                _searchValue,
                _fromPricePerQuantity, 
                _toPricePerQuantity,
                _selectedCategories
            ) ?? Enumerable.Empty<ProductView>();
        }

        private async Task OnSearchValueKeyUp(KeyboardEventArgs args)
        {
            if(args.Key == "Enter" || string.IsNullOrWhiteSpace(_searchValue))
            {
                await FetchProducts();
            }
        }

        private async Task OnSelectedCategoriesChanged(IEnumerable<string> categories)
        {
            _selectedCategories = categories;
            await FetchProducts();
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

        public void Dispose()
        {
            UsersBrowsingProducts.Dec();
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
