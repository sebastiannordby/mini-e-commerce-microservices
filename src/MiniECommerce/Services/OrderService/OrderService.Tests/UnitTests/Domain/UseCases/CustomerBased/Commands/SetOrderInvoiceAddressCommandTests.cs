using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Library.Events.OrderService;
using OrderService.Domain.Services;
using OrderService.Domain.UseCases.CustomerBased.Commands.SetInvoiceAddress;
using OrderService.Domain.UseCases.CustomerBased.Commands.Start;
using OrderService.Library.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Tests.UnitTests.Domain.UseCases.CustomerBased.Commands
{
    public class SetOrderInvoiceAddressCommandTests : BaseOrderServiceTest
    {
        [Test]
        public async Task TestCommand()
        {
            var harness = Services.GetRequiredService<ITestHarness>();
            var mediator = Services.GetRequiredService<MediatR.IMediator>();
            var orderService = Services.GetRequiredService<IOrderService>();
            var orderId = await mediator.Send(new StartOrderCommand());

            await harness.Start();
            var setAddress = await mediator.Send(new SetOrderInvoiceAddressCommand(
                AddressLine: "Hello",
                PostalCode: "Shwllo",
                PostalOffice: "Kellow",
                Country: "Mellow"
            ));
            await harness.Stop();

            var order = await orderService.FindAsync(orderId);

            Assert.That(orderId != Guid.Empty);
            Assert.That(setAddress.IsSuccess);
            Assert.That(await harness.Published.Any<OrderSetToWaitingForConfirmationEvent>());
            Assert.That(order.Status, Is.EqualTo(OrderStatus.WaitingForPayment));
        }
    }
}
