using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Library.Events.OrderService;
using OrderService.Domain.UseCases.CustomerBased.Commands.SetAddress;
using OrderService.Domain.UseCases.CustomerBased.Commands.Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Tests.UnitTests.Domain.UseCases.CustomerBased.Commands
{
    public class SetOrderAddressCommandTests : BaseOrderServiceTest
    {
        [Test]
        public async Task TestSetAddressReturnsTrue()
        {
            var harness = Services.GetRequiredService<ITestHarness>();
            var mediator = Services.GetRequiredService<MediatR.IMediator>();
            var orderId = await mediator.Send(new StartOrderCommand());

            await harness.Start();
            var setAddress = await mediator.Send(new SetOrderAddressCommand(
                AddressLine: "Hello",
                PostalCode: "Shwllo",
                PostalOffice: "Kellow",
                Country: "Mellow"
            ));
            await harness.Stop();

            Assert.AreNotEqual(orderId, Guid.Empty);
            Assert.IsTrue(setAddress.IsSuccess);
            Assert.IsTrue(await harness.Published.Any<OrderSetToWaitingForConfirmationEvent>());
        }
    }
}
