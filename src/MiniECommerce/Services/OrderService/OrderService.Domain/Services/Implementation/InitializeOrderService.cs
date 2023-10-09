using OrderService.Domain.Exceptions;
using OrderService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Services.Implementation
{
    internal class InitializeOrderService : IInitializeOrderService
    {
        private readonly IOrderService _orderService;

        public InitializeOrderService(
            IOrderService orderService)
        {
            _orderService = orderService;
        }
            
        public async Task<Order> Initialize(
            string buyersFullName,
            string buyersEmailAddress)
        {
            var hasOrderInProgress = await _orderService.HasOrderInProgress(buyersEmailAddress);
            if (hasOrderInProgress)
                throw new AlreadyHasOrderInProgressException(
                    "Cannot create an order when you already have one in progress.");

            var newNumber = await _orderService.GetNewNumber();

            return new Order(newNumber, buyersFullName, buyersEmailAddress);
        }
    }
}
