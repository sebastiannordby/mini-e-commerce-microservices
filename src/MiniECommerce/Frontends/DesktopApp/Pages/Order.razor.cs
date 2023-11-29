using Microsoft.AspNetCore.Components;
using MiniECommerce.Consumption.Repositories.OrderService;
using MudBlazor;
using OrderService.Library.Commands;
using OrderService.Library.Enumerations;
using OrderService.Library.Models;
using Prometheus;
using System.Text.RegularExpressions;

namespace DesktopApp.Pages
{
    public partial class Order : ComponentBase, IDisposable
    {
        private OrderView? _currentOrder;
        private bool _isFormDataValid;
        private string[] _formErrors = { };
        private MudTextField<string> pwField1;
        private MudForm form;

        private SetOrderDeliveryAddressCommandDto _setDeliveryAddressCommand;
        private SetOrderInvoiceAddressCommandDto _setInvoiceAddressCommand;

        public static readonly Gauge UsersOrderingGauge = Metrics.CreateGauge(
            "users_ordering",
            "Users checking or placing an order.");

        protected override async Task OnInitializedAsync()
        {
            var startedOrderId = await OrderRepository.GetStartedOrder();
            if(!startedOrderId.HasValue)
            {
                NavigationManager.NavigateTo("/orders");
                return;
            }

            _currentOrder = await OrderRepository.Get(startedOrderId.Value);
            if (_currentOrder is null)
            {
                NavigationManager.NavigateTo("/orders");
                return;
            }

            await ConfigureCommandsRelativeToStatus(_currentOrder);
            UsersOrderingGauge.Inc();
        }

        private Task ConfigureCommandsRelativeToStatus(OrderView order)
        {
            if(order.Status == OrderStatus.WaitingForDeliveryAddress)
            {
                _setDeliveryAddressCommand = new SetOrderDeliveryAddressCommandDto()
                {
                    OrderId = order.Id,
                    AddressLine = order.DeliveryAddressLine ?? string.Empty,
                    Country = order.DeliveryAddressCountry ?? string.Empty,
                    PostalCode = order.DeliveryAddressPostalCode ?? string.Empty,
                    PostalOffice = order.DeliveryAddressPostalOffice ?? string.Empty
                };
            }
            else if(order.Status == OrderStatus.WaitingForInvoiceAddress)
            {
                _setInvoiceAddressCommand = new SetOrderInvoiceAddressCommandDto()
                {
                    OrderId = order.Id,
                    AddressLine = order.InvoiceAddressLine ?? string.Empty,
                    Country = order.InvoiceAddressCountry ?? string.Empty,
                    PostalCode = order.InvoiceAddressPostalCode ?? string.Empty,
                    PostalOffice = order.InvoiceAddressPostalOffice ?? string.Empty
                };
            }

            return Task.CompletedTask;
        }

        private async Task TryExecuteSetDeliveryAddress()
        {
            await form.Validate();
            if (!form.IsValid)
                return;

            var setAddressResult = await OrderRepository
                .SetDeliveryAddress(_setDeliveryAddressCommand);
            if(!setAddressResult.IsSuccess)
            {
                await DialogService.ShowMessageBox(
                    "Could not set address", 
                    String.Join(Environment.NewLine, setAddressResult.Errors));
                return;
            }

            Snackbar.Add("Order successfully filled. Wait for confirmation..");
            await RefetchOrder();
        }

        private async Task TryExecuteSetInvoiceAddress()
        {
            await form.Validate();
            if (!form.IsValid)
                return;

            var setAddressResult = await OrderRepository
                .SetInvoiceAddress(_setInvoiceAddressCommand);
            if (!setAddressResult.IsSuccess)
            {
                await DialogService.ShowMessageBox(
                    "Could not set address",
                    String.Join(Environment.NewLine, setAddressResult.Errors));
                return;
            }

            Snackbar.Add("Order successfully filled. Wait for confirmation..");
            await RefetchOrder();
        }

        private async Task RefetchOrder()
        {
            _currentOrder = _currentOrder is not null ? 
                await OrderRepository.Get(_currentOrder.Id) : null;
        }

        public void Dispose()
        {
            UsersOrderingGauge.Dec();
        }

        [Inject] public required IDialogService DialogService { get; set; }
        [Inject] public required ISnackbar Snackbar { get; set; }
        [Inject] public required NavigationManager NavigationManager { get; set; }
        [Inject] public required IOrderRepository OrderRepository { get; set; }
    }
}
