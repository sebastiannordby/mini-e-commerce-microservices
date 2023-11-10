using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Authentication.Services;
using OrderService.Domain.Repositories;
using OrderService.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Tests.UnitTests.Domain.Services
{
    public class InitializeOrderServiceTests : BaseOrderServiceTest
    {
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
        public async Task TestFindOrders()
        {
            var initOrderService = Services.GetRequiredService<IInitializeOrderService>();
            var orderService = Services.GetRequiredService<IOrderService>();
            var orderViewRepository = Services.GetRequiredService<IOrderViewRepository>();
            var currentUserService = Services.GetRequiredService<ICurrentUserService>();

            var order1ToSave = await initOrderService.Initialize(
                currentUserService.UserFullName, currentUserService.UserEmail);
            var order1Id = await orderService.Save(order1ToSave);

            await orderService.SetWaitingForConfirmation(
                currentUserService.UserEmail);

            var order2ToSave = await initOrderService.Initialize(
                currentUserService.UserFullName, currentUserService.UserEmail);
            var order2Id = await orderService.Save(order1ToSave);

            var orders = await orderViewRepository.List(
                currentUserService.UserEmail);

            Assert.IsNotNull(orders);
            Assert.IsNotEmpty(orders);
            Assert.True(orders.Count() == 2);
        }
    }
}
