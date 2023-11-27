using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MiniECommerce.Consumption.Repositories.OrderService.Administration;
using MudBlazor;
using OrderService.Library.Commands;
using OrderService.Library.Enumerations;
using OrderService.Library.Models;

namespace DesktopApp.Pages.Admin
{
    public partial class OrderManagement : ComponentBase
    {
        private IEnumerable<OrderView>? _orders;

        private bool _showSetOrderAddressDialog;
        private OrderView? _setOrderAddressDialog;
        private SetOrderDeliveryAddressCommandDto? _setOrderAddressCommand;

        protected override async Task OnInitializedAsync()
        {
            await FetchOrders();
        }

        private async Task FetchOrders()
        {
            _orders = await OrderRepository.List();
        }

        private void ShowSetAddressDialog(OrderView order)
        {
            _setOrderAddressCommand = new(
                order.Id,
                order.AddressLine ?? string.Empty,
                order.PostalCode ?? string.Empty,
                order.PostalOffice ?? string.Empty,
                order.Country ?? string.Empty
            );
            _setOrderAddressDialog = order;
            _showSetOrderAddressDialog = true;
        }

        private async Task ExecuteSetAddress()
        {
            if (_setOrderAddressCommand is null || 
                _setOrderAddressDialog is null)
                return;

            var result = await OrderRepository.SetDeliveryAddress(_setOrderAddressCommand);
            if (!result)
            {
                Snackbar.Add($"Order({_setOrderAddressDialog.Number}) could not update address.");
                return;
            }

            Snackbar.Add($"Order({_setOrderAddressDialog.Number}) address updated successfully.");
            _showSetOrderAddressDialog = false;
            _setOrderAddressCommand = null;
            _setOrderAddressDialog = null;
            await FetchOrders();
        }

        private async Task ConfirmOrder(OrderView order)
        {
            if(order.Status != OrderStatus.WaitingForConfirmation)
            {
                Snackbar.Add($"Order({order.Number}) is not waiting for confirmation, and therefore cannot be confirmed.");
                return;
            }

            var res = await OrderRepository.Confirm(order.Id);
            if(!res)
            {
                Snackbar.Add($"Order({order.Number}) could not be confirmed. Try again..");
                return;
            }

            Snackbar.Add($"Order({order.Number}) successfully confirmed.");
            await FetchOrders();
        }

        private async Task SetToWaitingForConfirmation(OrderView order)
        {
            if(order.Status > OrderStatus.WaitingForConfirmation)
            {
                Snackbar.Add($"Order({order.Number}) must have status less than waiting for configuration.");
                return;
            }

            var res = await OrderRepository.SetWaitingForConfirmation(order.Id);
            if (!res)
            {
                Snackbar.Add($"Order({order.Number}) could not change order status.. Try again..");
                return;
            }

            Snackbar.Add($"Order({order.Number}) Order successfully set to waiting for confirmation.");
            await FetchOrders();
        }

        [Inject] public required ISnackbar Snackbar { get; set; }
        [Inject] public required IOrderAdminRepository OrderRepository { get; set; }
    }
}
