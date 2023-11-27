using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Library.Events.OrderService;
using OrderService.Domain.UseCases.CustomerBased.Commands.SetDeliveryAddress;
using OrderService.Domain.UseCases.CustomerBased.Commands.Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Tests.UnitTests.Domain.UseCases.CustomerBased.Commands
{
    public class SetOrderDeliveryAddressCommandTests : BaseOrderServiceTest
    {
        [Test]
        public async Task TestCommand()
        {
            var mediator = Services.GetRequiredService<MediatR.IMediator>();
            var orderId = await mediator.Send(new StartOrderCommand());

            var setAddress = await mediator.Send(new SetOrderDeliveryAddressCommand(
                AddressLine: "Hello",
                PostalCode: "Shwllo",
                PostalOffice: "Kellow",
                Country: "Mellow"
            ));

            Assert.AreNotEqual(orderId, Guid.Empty);
            Assert.IsTrue(setAddress.IsSuccess);
        }
    }
}
