using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Library.Events.OrderService;
using OrderService.Domain.UseCases.CustomerBased.Commands.Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Tests.UnitTests.Domain.UseCases.CustomerBased.Commands
{
    public class StartOrderCommandTest : BaseOrderServiceTest
    {
        [Test]
        public async Task TestNotReturnEmptyOrderId()
        {
            var mediator = Services.GetRequiredService<MediatR.IMediator>();
            var orderId = await mediator.Send(new StartOrderCommand());

            Assert.That(orderId, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public async Task TestOrderStartedEventPublished()
        {
            var harness = Services.GetRequiredService<ITestHarness>();
            var mediator = Services.GetRequiredService<MediatR.IMediator>();

            await harness.Start();
            var orderId = await mediator.Send(new StartOrderCommand());
            await harness.Stop();

            Assert.That(await harness.Published.Any<OrderStartedEvent>());
        }
    }
}
