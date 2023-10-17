using OrderService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Repositories
{
    public interface IOrderViewRepository
    {
        Task<OrderView?> Find(Guid id);
    }
}
