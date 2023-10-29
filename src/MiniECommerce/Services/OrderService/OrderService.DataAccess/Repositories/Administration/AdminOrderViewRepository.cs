using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderService.DataAccess.Extensions;
using OrderService.Domain.Repositories;
using OrderService.Library.Models;

namespace OrderService.DataAccess.Repositories.Administration
{
    internal class AdminOrderViewRepository : IAdminOrderViewRepository
    {
        private readonly OrderDbContext _dbContext;

        public AdminOrderViewRepository(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<OrderView>> List()
        {
            var orderViews = await _dbContext.Orders
                .AsViewQuery(_dbContext)
                .ToListAsync();

            return orderViews;
        }
    }
}
