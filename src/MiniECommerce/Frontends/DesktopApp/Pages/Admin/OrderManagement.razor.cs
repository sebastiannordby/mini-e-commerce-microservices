using Microsoft.AspNetCore.Components;
using MiniECommerce.Consumption.Repositories.OrderService.Administration;
using OrderService.Library.Models;

namespace DesktopApp.Pages.Admin
{
    public partial class OrderManagement : ComponentBase
    {
        private IEnumerable<OrderView>? _orders;

        private bool _showSetOrderAddressDialog;
        private OrderView _setOrderAddressDialog;

        protected override async Task OnInitializedAsync()
        {
            _orders = await OrderRepository.List();
        }
       
        private void ShowSetAddressDialog(OrderView order)
        {

        }

        private async Task ConfirmOrder(OrderView order)
        {

        }

        [Inject] public required IOrderAdminRepository OrderRepository { get; set; }
    }
}
