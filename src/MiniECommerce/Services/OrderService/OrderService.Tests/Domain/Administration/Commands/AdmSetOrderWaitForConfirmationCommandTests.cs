using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Domain.Repositories;
using OrderService.Domain.Services;
using OrderService.Domain.UseCases.Administration.Commands.Confirm;
using OrderService.Domain.UseCases.Administration.Commands.SetWaitForConfirmation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Tests.Domain.Administration.Commands
{
    public class AdmSetOrderWaitForConfirmationCommandTests : BaseOrderServiceTest
    {
        [Test]
        public async Task TestCanOnlyWaitForConfirmationUnderCertainCrit()
        {
            var mediator = Services.GetRequiredService<IMediator>();
            var orderService = Services.GetRequiredService<IOrderService>();
            var orderViewRepository = Services.GetRequiredService<IOrderViewRepository>();
            var initOrderService = Services.GetRequiredService<IInitializeOrderService>();

            var orderToSave = await initOrderService.Initialize(
                nameof(TestCanOnlyWaitForConfirmationUnderCertainCrit), nameof(TestCanOnlyWaitForConfirmationUnderCertainCrit));

            var orderId = await orderService.Save(orderToSave);

            var waitingForConfirmationRes1 = await mediator.Send(
                new AdmSetOrderWaitForConfirmationCommand(orderId));

            var confirmedOrderRes = await mediator.Send(new AdmConfirmOrderCommand(orderId));

            Assert.IsTrue(waitingForConfirmationRes1, "Waiting for confirmation 1");
            Assert.IsTrue(confirmedOrderRes, "Order confirmed");
            Assert.ThrowsAsync<Exception>(async () =>
            {
                var waitingForConfirmationRes2 = await mediator.Send(
                    new AdmSetOrderWaitForConfirmationCommand(orderId));
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

            Assert.IsTrue(waitingForConfirmationRes, "Set order to waiting for confirmation");
        }
    }
}
