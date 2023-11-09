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

        protected override async Task OnInitializedAsync()
        {
            var orders = (await OrderRepository.List()) ?? Enumerable.Empty<OrderView>();

            _activeOrders = orders
                .Where(x => x.Status <= OrderStatus.WaitingForConfirmation)
                .ToList();

            _historicOrders = orders
                .Where(x => x.Status > OrderStatus.WaitingForConfirmation)
                .ToList();

            _initialized = true;
        }

        [Inject] private IOrderRepository OrderRepository { get; set; }
    }
}
