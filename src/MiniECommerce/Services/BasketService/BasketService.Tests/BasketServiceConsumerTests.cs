using MassTransit.Testing;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Library.Events.BasketService;
using BasketService.Domain.Services;
using MiniECommerce.Library.Events.OrderService;

namespace BasketService.Tests
{
    internal class BasketServiceConsumerTests : BaseBasketServiceTest
    {
        [Test]
        public async Task TestAddingToBasketPublishesEvent()
        {
            var harness = Services.GetRequiredService<ITestHarness>();
            var basketService = Services.GetRequiredService<IUserBasketService>();

            await harness.Start();
            await basketService.AddToBasket(Guid.NewGuid(), "test@test.com", Guid.NewGuid());
            await harness.Stop();

            Assert.IsTrue(await harness.Published.Any<ProductAddedToBasketEvent>());
        }

        [Test]
        public async Task TestConsumingOrderStartedEvent()
        {
            var harness = Services.GetRequiredService<ITestHarness>();
            var basketService = Services.GetRequiredService<IUserBasketService>();
            var client = harness.GetRequestClient<OrderStartedEvent>();

            await harness.Start();
            var response = await client.GetResponse<BasketClearedEvent>(new OrderStartedEvent(
                Guid.NewGuid(), "test@test.com", DateTime.Now));
            await harness.Stop();

            Assert.NotNull(response.Message);
            Assert.IsTrue(await harness.Consumed.Any<OrderStartedEvent>());
        }

        [Test]
        public async Task TestConsumingOrderStartedEventWithoutResponse()
        {
            var harness = Services.GetRequiredService<ITestHarness>();
            var basketService = Services.GetRequiredService<IUserBasketService>();

            await harness.Start();
            await harness.Bus.Publish(new OrderStartedEvent(
                Guid.NewGuid(), "test@test.com", DateTime.Now));
            await harness.Stop();

            Assert.IsTrue(await harness.Consumed.Any<OrderStartedEvent>());
        }
    }
}
