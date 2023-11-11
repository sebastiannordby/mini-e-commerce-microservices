using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Authentication.Services;
using OrderService.Domain.Repositories;
using OrderService.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Tests.UnitTests.Domain.Repositories
{
    public class OrderViewRepositorTests : BaseOrderServiceTest
    {
        [Test]
        public async Task FindOrderTest()
        {
            var orderViewRepository = Services.GetRequiredService<IOrderViewRepository>();
            var order = await orderViewRepository.Find(Guid.Empty);

            Assert.IsNull(order);
        }

        [Test]
        public async Task FindOrdersTest()
        {
            var orderViewRepository = Services.GetRequiredService<IOrderViewRepository>();
            var orders = await orderViewRepository.List(string.Empty);

            Assert.IsNotNull(orders);
            Assert.IsEmpty(orders);
        }

        [Test]
        public async Task TestFindOrderView()
        {
            var initOrderService = Services.GetRequiredService<IInitializeOrderService>();
            var orderService = Services.GetRequiredService<IOrderService>();
            var orderViewRepository = Services.GetRequiredService<IOrderViewRepository>();
            var currentUserService = Services.GetRequiredService<ICurrentUserService>();

            var orderToSave = await initOrderService.Initialize(
                currentUserService.UserFullName, currentUserService.UserEmail);

            var orderId = await orderService.SaveAsync(orderToSave);
            var orderView = await orderViewRepository.Find(orderId);

            Assert.IsNotNull(orderView);
        }
    }
}
