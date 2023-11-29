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

        private bool _showSetDeliveryAddressDialog;
        private OrderView? _setDeliveryAddressForOrder;
        private SetOrderDeliveryAddressCommandDto? _setDeliveryAddressCommand;

        private bool _showSetInvoiceAddressDialog;
        private OrderView? _setInvoiceAddressForOrder;
        private SetOrderInvoiceAddressCommandDto? _setInvoiceAddressCommand;

        protected override async Task OnInitializedAsync()
        {
            await FetchOrders();
        }

        private async Task FetchOrders()
        {
            _orders = await OrderRepository.List();
        }

        private void ShowSetDeliveryAddressDialog(OrderView order)
        {
            _setDeliveryAddressCommand = new(
                order.Id,
                addressLine: order.DeliveryAddressLine ?? string.Empty,
                postalCode: order.DeliveryAddressPostalCode ?? string.Empty,
                postalOffice: order.DeliveryAddressPostalOffice ?? string.Empty,
                country: order.DeliveryAddressCountry ?? string.Empty
            );
            _setDeliveryAddressForOrder = order;
            _showSetDeliveryAddressDialog = true;
        }

        private void ShowSetInvoiceAddressDialog(OrderView order)
        {
            _setInvoiceAddressCommand = new(
                order.Id,
                addressLine: order.InvoiceAddressLine ?? string.Empty,
                postalCode: order.InvoiceAddressPostalCode ?? string.Empty,
                postalOffice: order.InvoiceAddressPostalOffice ?? string.Empty,
                country: order.InvoiceAddressCountry ?? string.Empty
            );
            _setInvoiceAddressForOrder = order;
            _showSetInvoiceAddressDialog = true;
        }
        
        private async Task ExecuteSetDeliveryAddress()
        {
            if (_setDeliveryAddressCommand is null || 
                _setDeliveryAddressForOrder is null)
                return;

            if (!await OrderRepository
                .SetDeliveryAddress(_setDeliveryAddressCommand))
            {
                Snackbar.Add($"Order({_setDeliveryAddressForOrder.Number}) could not update address.");
                return;
            }

            Snackbar.Add($"Order({_setDeliveryAddressForOrder.Number}) address updated successfully.");
            _showSetDeliveryAddressDialog = false;
            _setDeliveryAddressCommand = null;
            _setDeliveryAddressForOrder = null;
            await FetchOrders();
        }

        private async Task ExecuteSetInvoiceAddress()
        {
            if (_setInvoiceAddressCommand is null ||
                _setInvoiceAddressForOrder is null)
                return;

            if (!await OrderRepository
                    .SetInvoiceAddress(_setInvoiceAddressCommand))
            {
                Snackbar.Add($"Order({_setInvoiceAddressForOrder.Number}) could not update address.");
                return;
            }

            Snackbar.Add($"Order({_setInvoiceAddressForOrder.Number}) address updated successfully.");
            _showSetInvoiceAddressDialog = false;
            _setInvoiceAddressCommand = null;
            _setInvoiceAddressForOrder = null;
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
