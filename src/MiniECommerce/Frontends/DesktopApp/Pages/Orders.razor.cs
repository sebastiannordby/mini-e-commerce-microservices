using Microsoft.AspNetCore.Components;
using MiniECommerce.Consumption.Repositories.OrderService;
using OrderService.Library.Enumerations;
using OrderService.Library.Models;

namespace DesktopApp.Pages
{
    public partial class Orders : ComponentBase
    {
        private IEnumerable<OrderView>? _activeOrders;
        private IEnumerable<OrderView>? _historicOrders;
        private bool _initialized;

        private OrderView _orderToShowDetails;
        private bool _showOrderDetails;

        protected override async Task OnInitializedAsync()
        {
            var orders = (await OrderRepository.List()) ?? Enumerable.Empty<OrderView>();

            _activeOrders = orders
                .Where(x => x.Status <= OrderStatus.InShipping)
                .ToList();

            _historicOrders = orders
                .Where(x => x.Status > OrderStatus.InShipping)
                .ToList();

            _initialized = true;
        }

        private void ShowOrderDetails(OrderView order)
        {
            _orderToShowDetails = order;
            _showOrderDetails = true;
        }

        [Inject] private IOrderRepository OrderRepository { get; set; }
    }
}
