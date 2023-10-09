using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using OrderService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderService.Domain;
using OrderService.Domain.Services;

namespace OrderService.Tests.Domain
{
    public class OrderTests : BaseOrderServiceTest
    {
        [Test]
        public void TestLoadOrder()
        {
            var loadOrderService = Services.GetService<ILoadOrderService>();

            Assert.ThrowsAsync<ValidationException>(async() =>
            {
                await loadOrderService.LoadAsync(
                    id: Guid.Empty,
                    number: -1,
                    buyersName: null,
                    buyersEmailAddress: null,
                    addressLine: null,
                    postalCode: null,
                    postalOffice: null,
                    country: null,
                    orderLines: null
                );
            });
        }

        [Test]
        public async Task TestFindOrderIsNull()
        {
            var loadOrderService = Services.GetService<ILoadOrderService>();
            var orderService = Services.GetService<IOrderService>();
            var order = await orderService.FindAsync(Guid.Empty);

            Assert.IsNull(order);
        }

        [Test]
        public async Task TestFindOrder()
        {
            var initOrderService = Services.GetService<IInitializeOrderService>();
            var orderService = Services.GetService<IOrderService>();

            var orderToSave = await initOrderService.Initialize(
                nameof(TestFindOrder), nameof(TestFindOrder));

            var newOrderId = await orderService.Save(orderToSave);
            var order = await orderService.FindAsync(newOrderId);

            Assert.IsNotNull(order);
        }

    }
}
