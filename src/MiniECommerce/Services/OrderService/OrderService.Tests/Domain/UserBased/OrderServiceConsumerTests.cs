using MassTransit.Mediator;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Library.Events.OrderService;
using OrderService.Domain.UseCases.CustomerBased.Commands.Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Tests.Domain.UserBased
{
    public class OrderServiceConsumerTests : BaseOrderServiceTest
    {
        [Test]
        public async Task TestStartingOrderPublishesEvent()
        {
            var harness = Services.GetRequiredService<ITestHarness>();
            var mediator = Services.GetRequiredService<MediatR.IMediator>();

            await harness.Start();
            var orderId = await mediator.Send(new StartOrderCommand());
            await harness.Stop();

            Assert.IsTrue(await harness.Published.Any<OrderStartedEvent>());
        }
    }
}
