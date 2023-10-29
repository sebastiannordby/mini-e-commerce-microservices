using Microsoft.AspNetCore.Components;
using MiniECommerce.Consumption.Repositories.OrderService.Administration;
using OrderService.Library.Commands;
using OrderService.Library.Models;

namespace DesktopApp.Pages.Admin
{
    public partial class OrderManagement : ComponentBase
    {
        private IEnumerable<OrderView>? _orders;

        private bool _showSetOrderAddressDialog;
        private OrderView _setOrderAddressDialog;
        private SetOrderAddressCommandDto _setOrderAddressCommand;

        protected override async Task OnInitializedAsync()
        {
            _orders = await OrderRepository.List();
        }
       
        private void ShowSetAddressDialog(OrderView order)
        {
            _setOrderAddressCommand = new(
                order.AddressLine ?? string.Empty,
                order.PostalCode ?? string.Empty,
                order.PostalOffice ?? string.Empty,
                order.Country ?? string.Empty
            );
            _showSetOrderAddressDialog = true;
        }

        private async Task ConfirmOrder(OrderView order)
        {

        }

        [Inject] public required IOrderAdminRepository OrderRepository { get; set; }
    }
}
