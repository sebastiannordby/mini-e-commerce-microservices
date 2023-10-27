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

namespace BasketService.Tests
{
    public class ProductAddedToBasketEventTests : BaseBasketServiceTest
    {
        [Test]
        public async Task TestAddingToBasketPublishesEvent()
        {
            var harness = Services.GetRequiredService<ITestHarness>();
            var basketService = Services.GetService<IUserBasketService>();

            await harness.Start();
            await basketService.AddToBasket(Guid.NewGuid(), "test@test.com", Guid.NewGuid());
            await harness.Stop();

            Assert.IsTrue(await harness.Published.Any<ProductAddedToBasketEvent>());
        }
    }
}
