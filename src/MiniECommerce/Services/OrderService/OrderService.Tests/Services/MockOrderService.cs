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

        public Task<int> GetNewNumber()
        {
            return _orderService.GetNewNumber();
        }

        public Task<Guid?> GetStartedOrderId(string buyersEmailAddress)
        {
            return _orderService.GetStartedOrderId(buyersEmailAddress);
        }

        public Task<bool> HasOrderInProgress(string buyersEmailAddress)
        {
            return _orderService.HasOrderInProgress(buyersEmailAddress);
        }

        public Task<Guid> Save(Order order)
        {
            return _orderService.Save(order);
        }

        public async Task<bool> SetAddress(
            string buyersEmailAddress, 
            string addressLine, 
            string postalCode, 
            string postalOffice, 
            string country)
        {
            var startedOrderId = await GetStartedOrderId(buyersEmailAddress);
            var order = startedOrderId.HasValue ? _dbContext.Orders
                .FirstOrDefault(x => x.Id == startedOrderId) : null;
            if (order is null)
                return false;

            order.SetAddress(
                addressLine,
                postalCode,
                postalOffice,
                country);

            _dbContext.Orders.Update(order);
            _dbContext.SaveChanges();

            return true;
        }

        public async Task<bool> SetWaitingForConfirmation(string buyersEmailAddress)
        {
            var startedOrderId = await GetStartedOrderId(buyersEmailAddress);
            var order = startedOrderId.HasValue ? _dbContext.Orders
                .FirstOrDefault(x => x.Id == startedOrderId) : null;
            if (order is null)
                return false;

            order.SetStatus(OrderStatus.WaitingForConfirmation);
            _dbContext.Orders.Update(order);
            _dbContext.SaveChanges();

            return true;
        }
    }
}
