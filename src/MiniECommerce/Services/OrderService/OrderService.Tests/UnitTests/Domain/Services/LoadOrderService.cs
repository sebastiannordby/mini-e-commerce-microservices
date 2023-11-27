using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Domain.Models;
using OrderService.Domain.Services;
using OrderService.Library.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Tests.UnitTests.Domain.Services
{
    public class LoadOrderService : BaseOrderServiceTest
    {
        [Test]
        public void TestLoadInvalidOrderOrder()
        {
            var loadOrderService = Services.GetRequiredService<ILoadOrderService>();

            Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await loadOrderService.LoadAsync(
                    id: Guid.Empty,
                    number: -1,
                    status: OrderStatus.WaitingForDeliveryAddress,
                    buyersName: string.Empty,
                    buyersEmailAddress: string.Empty,
                    deliveryAddress: null,
                    deliveryAddressPostalCode: null,
                    deliveryAddressPostalOffice: null,
                    deliveryAddressCountry: null,
                    invoiceAddress: null,
                    invoiceAddressPostalCode: null,
                    invoiceAddressPostalOffice: null,
                    invoiceAddressCountry: null,
                    orderLines: new List<Order.OrderLine>()
                );
            });
        }

        [Test]
        public async Task TestFindOrderIsNull()
        {
            var loadOrderService = Services.GetRequiredService<ILoadOrderService>();
            var orderService = Services.GetRequiredService<IOrderService>();
            var order = await orderService.FindAsync(Guid.Empty);

            Assert.IsNull(order);
        }
    }
}
