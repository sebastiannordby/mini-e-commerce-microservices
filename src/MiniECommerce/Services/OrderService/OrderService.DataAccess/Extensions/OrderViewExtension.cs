using Microsoft.EntityFrameworkCore;
using OrderService.DataAccess.Models;
using OrderService.Library.Models;

namespace OrderService.DataAccess.Extensions
{
    internal static class OrderViewExtension
    {
        internal static IQueryable<OrderView> AsViewQuery(
            this IQueryable<OrderDao> query, OrderDbContext dbContext)
        {
            var viewQuery = 
                from order in query.AsNoTracking()

                let orderLines = dbContext.OrderLines
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
                        order.Status,
                        orderLines)
                ;

            return viewQuery;
        }
    }
}
