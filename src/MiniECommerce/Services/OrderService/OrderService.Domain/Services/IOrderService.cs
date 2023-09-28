using OrderService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OrderService.Domain.Models.Order;

namespace OrderService.Domain.Services
{
    public interface IOrderService
    {
        Task<Order> FindAsync(Guid id);
    }
}
