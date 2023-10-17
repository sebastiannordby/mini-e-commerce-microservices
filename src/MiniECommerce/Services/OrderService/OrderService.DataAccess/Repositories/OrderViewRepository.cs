using Microsoft.EntityFrameworkCore;
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
            var orderView = await (
                from order in _dbContext.Orders.AsNoTracking()
                
                let orderLines = _dbContext.OrderLines
                    .Where(x => x.OrderId == order.Id)
                    .Select(x => new OrderView.OrderLine(
                        x.Number,
                        x.ProductId,
                        x.ProductDescription,
                        x.Quantity,
                        x.PricePerQuantity
                    )).ToList()

                select new OrderView(
                    order.Id,
                    order.Number,
                    orderLines)
            ).FirstOrDefaultAsync();

            return orderView;
        }
    }
}
