using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using OrderService.Library.Commands;
using OrderService.Library.Models;

namespace MiniECommerce.Consumption.Repositories.OrderService
{
    public interface IOrderRepository
    {
        Task<OrderView?> Get(Guid orderId);
        Task<Guid?> Start(StartOrderCommandDto command);
        Task<Guid?> GetStartedOrder();
        Task<bool> SetAddress(SetOrderAddressCommandDto command);
    }
}
