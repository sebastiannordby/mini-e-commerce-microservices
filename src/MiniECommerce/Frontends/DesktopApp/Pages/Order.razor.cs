using Microsoft.AspNetCore.Components;
using MiniECommerce.Consumption.Repositories.OrderService;
using MudBlazor;
using OrderService.Library.Commands;
using OrderService.Library.Enumerations;
using OrderService.Library.Models;
using System.Text.RegularExpressions;

namespace DesktopApp.Pages
{
    public partial class Order : ComponentBase
    {
        private OrderView? _currentOrder;
        private bool _isFormDataValid;
        private string[] _formErrors = { };
        private MudTextField<string> pwField1;
        private MudForm form;

        private SetOrderAddressCommandDto _setAddressCommand;

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
        }

        private Task ConfigureCommandsRelativeToStatus(OrderView order)
        {
            if(order.Status == OrderStatus.InFill)
            {
                _setAddressCommand = new SetOrderAddressCommandDto()
                {
                    OrderId = order.Id,
                    AddressLine = string.Empty,
                    Country = string.Empty,
                    PostalCode = string.Empty,
                    PostalOffice = string.Empty
                };
            }

            return Task.CompletedTask;
        }

        private async Task TryExecuteSetAddress()
        {
            await form.Validate();
            if (!form.IsValid)
                return;

            var addressSetSuccessfully = await OrderRepository
                .SetAddress(_setAddressCommand);
            if(!addressSetSuccessfully)
            {
                Snackbar.Add("Could not update information.");
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

        [Inject] public required ISnackbar Snackbar { get; set; }
        [Inject] public required NavigationManager NavigationManager { get; set; }
        [Inject] public required IOrderRepository OrderRepository { get; set; }
    }
}
