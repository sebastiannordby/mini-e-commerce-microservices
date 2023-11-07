using Microsoft.AspNetCore.Components;
using MiniECommerce.Consumption.Repositories.OrderService;
using OrderService.Library.Models;

namespace DesktopApp.Pages
{
    public partial class Orders : ComponentBase
    {
        private readonly IEnumerable<OrderView>? _orders;

        protected override async Task OnInitializedAsync()
        {
            _orders = await OrderRepository.List();
        }

        [Inject] private IOrderRepository OrderRepository { get; set; }
    }
}
