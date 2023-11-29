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
                status: orderDao.Status,
                buyersName: orderDao.BuyersFullName,
                buyersEmailAddress: orderDao.BuyersEmailAddress,
                deliveryAddress: orderDao.DeliveryAddressLine,
                deliveryAddressPostalCode: orderDao.DeliveryAddressPostalCode,
                deliveryAddressPostalOffice: orderDao.DeliveryAddressPostalOffice,
                deliveryAddressCountry: orderDao.DeliveryAddressCountry,
                invoiceAddress: orderDao.InvoiceAddressLine,
                invoiceAddressPostalCode: orderDao.InvoiceAddressPostalCode,
                invoiceAddressPostalOffice: orderDao.InvoiceAddressPostalOffice,
                invoiceAddressCountry: orderDao.InvoiceAddressCountry,
                orderLines: orderLines);

            return order;
        }

        public async Task<int> GetNewNumberAsync()
        {
            var takenNumbers = _dbContext.Orders
                .Select(x => x.Number);

            return await takenNumbers.AnyAsync() ?
                await takenNumbers.MaxAsync() + 1 : 1;  
        }

        public async Task<Guid?> GetStartedOrderIdAsync(string buyersEmailAddress)
        {
            var orderId = await _dbContext.Orders
                .AsNoTracking()
                .Where(x => x.BuyersEmailAddress == buyersEmailAddress)
                .Where(x => x.Status < OrderStatus.WaitingForConfirmation)
                .Select(x => (Guid?) x.Id)
                .FirstOrDefaultAsync();

            return orderId;
        }

        public async Task<bool> HasOrderInProgressAsync(string buyersEmailAddress)
        {
            return await _dbContext.Orders
                .AsNoTracking()
                .Where(x => x.BuyersEmailAddress == buyersEmailAddress)
                .Where(x => x.Status < OrderStatus.WaitingForConfirmation)
                .AnyAsync();
        }

        public async Task<Guid> SaveAsync(Order order)
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

            orderDao.Update(order);
            await _dbContext.SaveChangesAsync();

            return orderDao.Id;
        }

        public async Task<bool> SetDeliveryAddressAsync(
            string buyersEmailAddress,
            string addressLine, 
            string postalCode, 
            string postalOffice, 
            string country)
        {
            if (string.IsNullOrWhiteSpace(buyersEmailAddress))
                return false;

            var res = await _dbContext.Orders
                .AsNoTracking()
                .Where(x => x.BuyersEmailAddress == buyersEmailAddress)
                .Where(x => x.Status <= OrderStatus.Confirmed)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(o => o.DeliveryAddressLine, addressLine)
                    .SetProperty(o => o.DeliveryAddressPostalCode, postalCode)
                    .SetProperty(o => o.DeliveryAddressPostalOffice, postalOffice)
                    .SetProperty(o => o.DeliveryAddressCountry, country));

            await _dbContext.SaveChangesAsync();

            return res > 0;
        }


        public async Task<bool> SetInvoiceAddressAsync(
            string buyersEmailAddress,
            string addressLine,
            string postalCode,
            string postalOffice,
            string country)
        {
            if (string.IsNullOrWhiteSpace(buyersEmailAddress))
                return false;

            var res = await _dbContext.Orders
                .AsNoTracking()
                .Where(x => x.BuyersEmailAddress == buyersEmailAddress)
                .Where(x => x.Status <= OrderStatus.Confirmed)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(o => o.InvoiceAddressLine, addressLine)
                    .SetProperty(o => o.InvoiceAddressPostalCode, postalCode)
                    .SetProperty(o => o.InvoiceAddressPostalOffice, postalOffice)
                    .SetProperty(o => o.InvoiceAddressCountry, country));

            await _dbContext.SaveChangesAsync();

            return res > 0;
        }

        public async Task<bool> SetWaitingForConfirmationAsync(string buyersEmailAddress)
        {
            if (string.IsNullOrWhiteSpace(buyersEmailAddress))
                return false;

            var res = await _dbContext.Orders
                .AsNoTracking()
                .Where(x => x.BuyersEmailAddress == buyersEmailAddress)
                .Where(x => x.Status <= OrderStatus.Confirmed)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(o => o.Status, OrderStatus.WaitingForConfirmation));

            await _dbContext.SaveChangesAsync();

            return res > 0;
        }
    }
}
