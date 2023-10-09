using OrderService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Services
{
    public interface IOrderService
    {
        Task<Order?> FindAsync(Guid id);
        Task<Guid> Save(Order order);
        Task<int> GetNewNumber();
        Task<bool> HasOrderInProgress(string buyersEmailAddress);
    }
}
