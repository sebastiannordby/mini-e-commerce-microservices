using OrderService.Library.Commands;
using OrderService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Consumption.Repositories.OrderService.Administration
{
    public interface IOrderAdminRepository
    {
        Task<IEnumerable<OrderView>> List();
        Task<bool> SetDeliveryAddress(SetOrderDeliveryAddressCommandDto command);
        Task<bool> SetInvoiceAddress(SetOrderInvoiceAddressCommandDto command);
        Task<bool> Confirm(Guid orderId);
        Task<bool> SetWaitingForConfirmation(Guid orderId);
    }
}
