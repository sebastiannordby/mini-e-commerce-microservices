using OrderService.DataAccess;
using OrderService.DataAccess.Services;
using OrderService.Domain.Models;
using OrderService.Domain.Services;
using OrderService.Library.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Tests.Services
{
    internal class MockOrderService : IOrderService
    {
        private readonly OrderDbContext _dbContext;
        private readonly ILoadOrderService _loadOrderService;
        private readonly OrderServiceImpl _orderService;

        public MockOrderService(
            OrderDbContext dbContext, 
            ILoadOrderService loadOrderService)
        {
            _dbContext = dbContext;
            _loadOrderService = loadOrderService;
            _orderService = new OrderServiceImpl(
                dbContext, loadOrderService);
        }

        public Task<Order?> FindAsync(Guid id)
        {
            return _orderService.FindAsync(id);
        }

        public Task<int> GetNewNumberAsync()
        {
            return _orderService.GetNewNumberAsync();
        }

        public Task<Guid?> GetStartedOrderIdAsync(string buyersEmailAddress)
        {
            return _orderService.GetStartedOrderIdAsync(buyersEmailAddress);
        }

        public Task<bool> HasOrderInProgressAsync(string buyersEmailAddress)
        {
            return _orderService.HasOrderInProgressAsync(buyersEmailAddress);
        }

        public Task<Guid> SaveAsync(Order order)
        {
            return _orderService.SaveAsync(order);
        }

        public async Task<bool> SetDeliveryAddressAsync(
            string buyersEmailAddress, 
            string addressLine, 
            string postalCode, 
            string postalOffice, 
            string country)
        {
            var startedOrderId = await GetStartedOrderIdAsync(buyersEmailAddress);
            var order = startedOrderId.HasValue ? _dbContext.Orders
                .FirstOrDefault(x => x.Id == startedOrderId) : null;
            if (order is null)
                return false;

            order.SetDeliveryAddress(
                addressLine,
                postalCode,
                postalOffice,
                country);

            _dbContext.Orders.Update(order);
            _dbContext.SaveChanges();

            return true;
        }

        public async Task<bool> SetInvoiceAddressAsync(string buyersEmailAddress, string addressLine, string postalCode, string postalOffice, string country)
        {
            var startedOrderId = await GetStartedOrderIdAsync(buyersEmailAddress);
            var order = startedOrderId.HasValue ? _dbContext.Orders
                .FirstOrDefault(x => x.Id == startedOrderId) : null;
            if (order is null)
                return false;

            order.SetInvoiceAddress(
                addressLine,
                postalCode,
                postalOffice,
                country);

            _dbContext.Orders.Update(order);
            _dbContext.SaveChanges();

            return true;
        }

        public async Task<bool> SetToWaitingForPaymentAsync(string buyersEmailAddress)
        {
            var startedOrderId = await GetStartedOrderIdAsync(buyersEmailAddress);
            var order = startedOrderId.HasValue ? _dbContext.Orders
                .FirstOrDefault(x => x.Id == startedOrderId) : null;
            if (order is null)
                return false;

            order.Status = OrderStatus.WaitingForPayment;
            _dbContext.Orders.Update(order);

            return true;
        }

        public async Task<bool> SetWaitingForConfirmationAsync(string buyersEmailAddress)
        {
            var startedOrderId = await GetStartedOrderIdAsync(buyersEmailAddress);
            var order = startedOrderId.HasValue ? _dbContext.Orders
                .FirstOrDefault(x => x.Id == startedOrderId) : null;
            if (order is null)
                return false;

            order.SetStatus(OrderStatus.WaitingForConfirmation);
            _dbContext.Orders.Update(order);
            _dbContext.SaveChanges();

            return true;
        }

        public Task<bool> SetWaitingForInvoiceAddressAsync(string buyersEmailAddress)
        {
            throw new NotImplementedException();
        }
    }
}
