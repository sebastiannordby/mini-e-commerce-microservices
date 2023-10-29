using Microsoft.EntityFrameworkCore;
using OrderService.DataAccess.Extensions;
using OrderService.Domain.Repositories;
using OrderService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.DataAccess.Repositories
{
    internal sealed class OrderViewRepository : IOrderViewRepository
    {
        private readonly OrderDbContext _dbContext;

        public OrderViewRepository(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OrderView?> Find(Guid id)
        {
            var orderView = await _dbContext.Orders
                .Where(x => x.Id == id)
                .AsViewQuery(_dbContext)
                .FirstOrDefaultAsync();

            return orderView;
        }
    }
}
