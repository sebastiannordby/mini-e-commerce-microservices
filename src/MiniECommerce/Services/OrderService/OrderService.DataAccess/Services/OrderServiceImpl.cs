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
                .Select(x => Order.OrderLine.Load(
                    x.Id,
                    x.Number,
                    x.ProductId,
                    x.ProductDescription,
                    x.Quantity,
                    x.PricePerQuantity)
                ).ToListAsync();

            var order = await _loadOrderService.LoadAsync(
                id: orderDao.Id,
                number: orderDao.Number, 
                buyersName: orderDao.BuyersFullName,
                addressLine: orderDao.AddressLine,
                postalCode: orderDao.PostalCode,
                postalOffice: orderDao.PostalOffice,
                country: orderDao.Country,
                orderLines: null);

            return order;
        }

        public async Task<Guid> Save(Order order)
        {
            var orderDao = await _dbContext.Orders
                .FirstOrDefaultAsync(x => x.Id == order.Id);

            if(orderDao == null)
            {
                var orderNumbers = _dbContext.Orders
                    .AsNoTracking()
                    .Select(x => x.Number);
                var orderNumber = orderNumbers.Any() ? 
                    orderNumbers.Max() + 1 : 1;

                orderDao = new Models.OrderDao(
                    id: Guid.NewGuid(),
                    number: orderNumber,
                    buyersFullName: order.BuyersName,
                    addressLine: order.AddressLine,
                    postalCode: order.PostalCode,
                    postalOffice: order.PostalOffice,
                    country: order.Country);

                _dbContext.Orders.Add(orderDao);
                _dbContext.SaveChanges();

                return orderDao.Id;
            }

            return Guid.Empty;
        }
    }
}
