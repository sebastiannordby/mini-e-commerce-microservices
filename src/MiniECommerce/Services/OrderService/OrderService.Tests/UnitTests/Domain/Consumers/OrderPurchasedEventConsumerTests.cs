using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Library.Events.OrderService;
using MiniECommerce.Library.Events.PurchaseService;
using OrderService.Domain.UseCases.CustomerBased.Commands.SetInvoiceAddress;
using OrderService.Domain.UseCases.CustomerBased.Commands.Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Tests.UnitTests.Domain.Consumers
{
    public class OrderPurchasedEventConsumerTests : BaseOrderServiceTest
    {
        [Test]
        public async Task TestConsumeEvent()
        {
            var harness = Services.GetRequiredService<ITestHarness>();
            var mediator = Services.GetRequiredService<MediatR.IMediator>();
            var bus = Services.GetRequiredService<IBus>();
            var orderId = await mediator.Send(new StartOrderCommand());

            await harness.Start();
            await bus.Publish(new OrderPurchasedEvent(Guid.Empty));
            await harness.Stop();

            Assert.That(await harness.Consumed.Any<OrderPurchasedEvent>());
        }
    }
}
