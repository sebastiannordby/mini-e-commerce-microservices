using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using OrderService.Library.Commands;

namespace MiniECommerce.Consumption.Repositories.OrderService
{
    public interface IOrderRepository
    {
        Task<Guid> Place(StartOrderCommandDto command);
    }
}
