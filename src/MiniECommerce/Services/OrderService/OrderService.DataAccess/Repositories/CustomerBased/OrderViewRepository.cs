using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using OrderService.DataAccess.Extensions;
using OrderService.Domain.Repositories;
using OrderService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.DataAccess.Repositories.CustomerBased
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

        public async Task<IEnumerable<OrderView>> List(string buyersEmail)
        {
            var orderViews = await _dbContext.Orders
                .Where(x => x.BuyersEmailAddress == buyersEmail)
                .AsViewQuery(_dbContext)
                .ToListAsync();

            return orderViews;
        }
    }
}
