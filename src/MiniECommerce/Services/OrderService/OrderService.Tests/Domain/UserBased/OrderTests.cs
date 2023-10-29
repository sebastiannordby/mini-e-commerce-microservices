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
using MediatR;

namespace OrderService.Tests.Domain.UserBased
{
    public class OrderTests : BaseOrderServiceTest
    {
        [Test]
        public async Task FindOrderTest()
        {
            var orderViewRepository = Services.GetRequiredService<IOrderViewRepository>();
            var order = await orderViewRepository.Find(Guid.Empty);

            Assert.IsNull(order);
        }

        [Test]
        public void TestLoadInvalidOrderOrder()
        {
            var loadOrderService = Services.GetRequiredService<ILoadOrderService>();

            Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await loadOrderService.LoadAsync(
                    id: Guid.Empty,
                    number: -1,
                    buyersName: string.Empty,
                    buyersEmailAddress: string.Empty,
                    addressLine: null,
                    postalCode: null,
                    postalOffice: null,
                    country: null,
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

        [Test]
        public async Task TestFindOrder()
        {
            var initOrderService = Services.GetRequiredService<IInitializeOrderService>();
            var orderService = Services.GetRequiredService<IOrderService>();

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
            var initOrderService = Services.GetRequiredService<IInitializeOrderService>();
            var orderService = Services.GetRequiredService<IOrderService>();
            var orderViewRepository = Services.GetRequiredService<IOrderViewRepository>();

            var orderToSave = await initOrderService.Initialize(
                nameof(TestFindOrder), nameof(TestFindOrder));

            var orderId = await orderService.Save(orderToSave);
            var orderView = await orderViewRepository.Find(orderId);

            Assert.IsNotNull(orderView);
        }
    }
}
