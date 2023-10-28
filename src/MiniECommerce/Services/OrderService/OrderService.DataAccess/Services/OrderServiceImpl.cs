using Microsoft.EntityFrameworkCore;
using MiniECommerce.Authentication.Services;
using OrderService.DataAccess.Models;
using OrderService.Domain.Models;
using OrderService.Domain.Services;
using OrderService.Library.Enumerations;
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

        public async Task<Order?> FindAsync(Guid id)
        {
            var orderDao = await _dbContext.Orders
                .FindAsync(id);
            if (orderDao == null)
                return null;

            var orderLines = await _dbContext.OrderLines
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
                buyersEmailAddress: orderDao.BuyersEmailAddress,
                addressLine: orderDao.AddressLine,
                postalCode: orderDao.PostalCode,
                postalOffice: orderDao.PostalOffice,
                country: orderDao.Country,
                orderLines: orderLines);

            return order;
        }

        public async Task<int> GetNewNumber()
        {
            var takenNumbers = _dbContext.Orders
                .Select(x => x.Number);

            return await takenNumbers.AnyAsync() ?
                await takenNumbers.MaxAsync() + 1 : 1;  
        }

        public async Task<Guid?> GetStartedOrderId(string buyersEmailAddress)
        {
            return await _dbContext.Orders
                .AsNoTracking()
                .Where(x => x.BuyersEmailAddress == buyersEmailAddress)
                .Where(x => x.Status <= OrderStatus.Confirmed)
                .Select(x => (Guid?) x.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> HasOrderInProgress(string buyersEmailAddress)
        {
            return await _dbContext.Orders
                .AsNoTracking()
                .Where(x => x.BuyersEmailAddress == buyersEmailAddress)
                .AnyAsync();
        }

        public async Task<Guid> Save(Order order)
        {
            var orderDao = await _dbContext.Orders
                .FirstOrDefaultAsync(x => x.Id == order.Id);

            if(orderDao is null)
            {
                var orderNumbers = _dbContext.Orders
                    .AsNoTracking()
                    .Select(x => x.Number);
                var orderNumber = orderNumbers.Any() ? 
                    orderNumbers.Max() + 1 : 1;

                orderDao = new OrderDao(order);

                await _dbContext.Orders.AddAsync(orderDao);
                await _dbContext.SaveChangesAsync();

                return orderDao.Id;
            }
            else
            {
                orderDao.Update(order);
                await _dbContext.SaveChangesAsync();
            }

            return Guid.Empty;
        }
    }
}
