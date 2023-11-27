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
            return await Initialize(
                buyersFullName,
                buyersEmailAddress,
                deliveryAddress: null,
                deliveryAddressPostalCode: null,
                deliveryAddressPostalOffice: null,
                deliveryAddressCountry: null,
                invoiceAddress: null,
                invoiceAddressPostalCode: null,
                invoiceAddressPostalOffice: null,
                invoiceAddressCountry: null);
        }

        public async Task<Order> Initialize(
            string buyersFullName,
            string buyersEmailAddress,
            string? deliveryAddress,
            string? deliveryAddressPostalCode,
            string? deliveryAddressPostalOffice,
            string? deliveryAddressCountry,
            string? invoiceAddress,
            string? invoiceAddressPostalCode,
            string? invoiceAddressPostalOffice,
            string? invoiceAddressCountry)
        {
            var hasOrderInProgress = await _orderService.HasOrderInProgressAsync(buyersEmailAddress);
            if (hasOrderInProgress)
                throw new AlreadyHasOrderInProgressException(
                    "Cannot create an order when you already have one in progress.");

            var newNumber = await _orderService.GetNewNumberAsync();

            return new Order(
                newNumber, 
                buyersFullName, 
                buyersEmailAddress,
                deliveryAddress,
                deliveryAddressPostalCode,
                deliveryAddressPostalOffice,
                deliveryAddressCountry,
                invoiceAddress,
                invoiceAddressPostalCode,
                invoiceAddressPostalOffice,
                invoiceAddressCountry);
        }
    }
}
