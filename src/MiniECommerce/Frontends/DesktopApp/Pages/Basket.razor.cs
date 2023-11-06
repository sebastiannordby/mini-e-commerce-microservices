using BasketService.Library;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MiniECommerce.Consumption.Repositories.BasketService;
using MiniECommerce.Consumption.Repositories.OrderService;
using MudBlazor;
using OrderService.Library.Commands;
using Prometheus;
using System.Security.Claims;

namespace DesktopApp.Pages
{
    public partial class Basket : ComponentBase, IDisposable
    {
        private List<BasketItemView> _basketItems = new();
        private bool _initialized;

        private string UserEmail =>
            HttpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value ?? "";

        public static readonly Gauge UsersInBasketGauge = Metrics.CreateGauge(
            "users_in_basket",
            "Counting users active in basket.");

        protected override async Task OnInitializedAsync()
        {
            await FetchBasket();
            _initialized = true;
            UsersInBasketGauge.Inc();
        }

        private async Task FetchBasket()
        {
            _basketItems = await BasketRepository.GetBasket();
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
            NavigationManager.NavigateTo("/order");
        }

        public void Dispose()
        {
            UsersInBasketGauge.Dec();
        }

        [Inject] public required NavigationManager NavigationManager { get; set; }
        [Inject] public required IJSRuntime JSRuntime { get; set; }
        [Inject] public required IHttpContextAccessor HttpContextAccessor { get; set; }
        [Inject] public required IBasketRepository BasketRepository { get; set; }
        [Inject] public required ISnackbar Snackbar { get; set; }
        [Inject] public required IOrderRepository OrderRepository { get; set; }
    }
}
