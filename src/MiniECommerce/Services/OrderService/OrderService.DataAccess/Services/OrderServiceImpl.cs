using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Models;
using OrderService.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.DataAccess.Services
{
    internal sealed class OrderServiceImpl : IOrderService
    {
        private readonly OrderDbContext _dbContext;
        private readonly ILoadOrderService _loadOrderService;

        public OrderServiceImpl(
            OrderDbContext dbContext, 
            ILoadOrderService loadOrderService)
        {
            _dbContext = dbContext;
            _loadOrderService = loadOrderService;
        }

        public async Task<Order> FindAsync(Guid id)
        {
            var orderDao = await _dbContext.Orders
                .FindAsync(id);
            if (orderDao == null)
                return null;

            var orderLinesDao = await _dbContext.OrderLines
                .Where(x => x.OrderId == id)
                .ToListAsync();

            var order = await _loadOrderService.LoadAsync(
                id: orderDao.Id,
                number: orderDao.Number,
                orderLines: null);

            return order;
        }
    }
}
