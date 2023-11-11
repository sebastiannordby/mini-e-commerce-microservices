using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Library.Events.OrderService;
using NSubstitute;
using OrderService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Tests.Domain.Consumers
{
    public class OrderSetToWaitingForConfirmationConsumerTests : BaseProductServiceTest
    {
        [Test]
        public async Task TestConsumingEvent()
        {
            var harness = Services.GetRequiredService<ITestHarness>();
            var mediator = Services.GetRequiredService<MediatR.IMediator>();

            await harness.Start();
            await harness.Bus.Publish(new OrderSetToWaitingForConfirmationEvent(
                Substitute.For<OrderView>()));
            await harness.Stop();

            Assert.IsTrue(await harness.Published.Any<OrderSetToWaitingForConfirmationEvent>());
            Assert.IsTrue(await harness.Consumed.Any<OrderSetToWaitingForConfirmationEvent>());
        }
    }
}
