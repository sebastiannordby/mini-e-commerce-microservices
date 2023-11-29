using FluentValidation;
using OrderService.Domain.Models;
using OrderService.Domain.Services;
using OrderService.Library.Enumerations;
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
        public async Task<Order> LoadAsync(
            Guid id, 
            int number,
            OrderStatus status,
            string buyersName,
            string buyersEmailAddress,
            string? deliveryAddress,
            string? deliveryAddressPostalCode,
            string? deliveryAddressPostalOffice,
            string? deliveryAddressCountry,
            string? invoiceAddress,
            string? invoiceAddressPostalCode,
            string? invoiceAddressPostalOffice,
            string? invoiceAddressCountry,
            IEnumerable<OrderLine> orderLines)
        {
            var order = new Order(
                id: id,
                number: number,
                status: status,
                buyersName: buyersName,
                buyersEmailAddress: buyersEmailAddress,
                deliveryAddress: deliveryAddress,
                deliveryAddressPostalCode: deliveryAddressPostalCode,
                deliveryAddressPostalOffice: deliveryAddressPostalOffice,
                deliveryAddressCountry: deliveryAddressCountry,
                invoiceAddress: invoiceAddress,
                invoiceAddressPostalCode: invoiceAddressPostalCode,
                invoiceAddressPostalOffice: invoiceAddressPostalOffice,
                invoiceAddressCountry: invoiceAddressCountry,
                orderLines: orderLines);

            var validationResult = order.Validate();
            if (validationResult.Any())
                throw new ValidationException(validationResult);

            return await Task.FromResult(order);
        }
    }
}
