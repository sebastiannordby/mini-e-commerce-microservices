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
using OrderService.Domain.Repositories;

namespace OrderService.Tests.Domain
{
    public class OrderTests : BaseOrderServiceTest
    {
        [Test]
        public async Task FindOrderTest()
        {
            var orderViewRepository = Services.GetService<IOrderViewRepository>();
            var order = await orderViewRepository.Find(Guid.Empty);

            Assert.IsNull(order);
        }

        [Test]
        public void TestLoadInvalidOrderOrder()
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
            Assert.That((int)orderToSave.Status == (int)order.Status);
            Assert.That(orderToSave.Number == order.Number);
        }

        [Test]
        public async Task TestFindOrderView()
        {
            var initOrderService = Services.GetService<IInitializeOrderService>();
            var orderService = Services.GetService<IOrderService>();
            var orderViewRepository = Services.GetService<IOrderViewRepository>();

            var orderToSave = await initOrderService.Initialize(
                nameof(TestFindOrder), nameof(TestFindOrder));

            var orderId = await orderService.Save(orderToSave);
            var orderView = await orderViewRepository.Find(orderId);

            Assert.IsNotNull(orderView);
        }
    }
}
