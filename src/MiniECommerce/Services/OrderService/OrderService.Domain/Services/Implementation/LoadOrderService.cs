using FluentValidation;
using OrderService.Domain.Models;
using OrderService.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OrderService.Domain.Models.Order;

namespace OrderService.Domain.Services.Implementation
{
    internal class LoadOrderService : ILoadOrderService
    {
        public async Task<Order> LoadAsync(Guid id, int number, IEnumerable<OrderLine> orderLines)
        {
            var order = new Order(
                id: id,
                number: number,
                orderLines: orderLines);

            var validationResult = order.Validate();
            if (validationResult.Any())
                throw new ValidationException(validationResult);

            return await Task.FromResult(order);
        }
    }
}
