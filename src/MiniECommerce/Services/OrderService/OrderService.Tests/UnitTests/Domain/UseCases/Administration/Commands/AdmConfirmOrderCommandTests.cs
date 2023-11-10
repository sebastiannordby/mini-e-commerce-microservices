using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Domain.Repositories;
using OrderService.Domain.Services;
using OrderService.Domain.UseCases.Administration.Commands.Confirm;
using OrderService.Domain.UseCases.Administration.Commands.SetWaitForConfirmation;
using OrderService.Domain.UseCases.Administration.Queries.ListOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Tests.UnitTests.Domain.UseCases.Administration.Commands
{
    public class AdmConfirmOrderCommandTests : BaseOrderServiceTest
    {
        [Test]
        public async Task TestCanOnlyConfirmUnderCertainCrit()
        {
            var mediator = Services.GetRequiredService<IMediator>();
            var orderService = Services.GetRequiredService<IOrderService>();
            var orderViewRepository = Services.GetRequiredService<IOrderViewRepository>();
            var initOrderService = Services.GetRequiredService<IInitializeOrderService>();

            var orderToSave = await initOrderService.Initialize(
                nameof(TestCanOnlyConfirmUnderCertainCrit), nameof(TestCanOnlyConfirmUnderCertainCrit));

            var orderId = await orderService.Save(orderToSave);

            Assert.ThrowsAsync<Exception>(async () =>
            {
                await mediator.Send(new AdmConfirmOrderCommand(orderId));
            });
        }

        [Test]
        public async Task TestCanConfirmOrder()
        {
            var mediator = Services.GetRequiredService<IMediator>();
            var orderService = Services.GetRequiredService<IOrderService>();
            var orderViewRepository = Services.GetRequiredService<IOrderViewRepository>();
            var initOrderService = Services.GetRequiredService<IInitializeOrderService>();

            var orderToSave = await initOrderService.Initialize(
                nameof(TestCanConfirmOrder), nameof(TestCanConfirmOrder));

            var orderId = await orderService.Save(orderToSave);

            var waitingForConfirmationRes = await mediator.Send(
                new AdmSetOrderWaitForConfirmationCommand(orderId));

            var confirmedOrderRes = await mediator.Send(new AdmConfirmOrderCommand(orderId));

            Assert.IsTrue(waitingForConfirmationRes, "Set order to waiting for confirmation");
            Assert.IsTrue(confirmedOrderRes, "Confirmed the order");
        }
    }
}
