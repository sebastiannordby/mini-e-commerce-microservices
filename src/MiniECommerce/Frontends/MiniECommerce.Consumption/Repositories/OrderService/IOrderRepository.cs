using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using OrderService.Library.Commands;
using OrderService.Library.Models;
using FluentResults;

namespace MiniECommerce.Consumption.Repositories.OrderService
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderView>> List();
        Task<OrderView?> Get(Guid orderId);
        Task<Guid?> Start();
        Task<Guid?> GetStartedOrder();
        Task<Result> SetDeliveryAddress(SetOrderDeliveryAddressCommandDto command);
        Task<Result> SetInvoiceAddress(SetOrderInvoiceAddressCommandDto command);
    }
}
