
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiniECommerce.Library.Responses;
using OrderService.Domain.UseCases.Administration.Commands.Confirm;
using OrderService.Domain.UseCases.Administration.Commands.SetAddress;
using OrderService.Domain.UseCases.Administration.Commands.SetWaitForConfirmation;
using OrderService.Domain.UseCases.Administration.Queries.ListOrders;
using OrderService.Library.Commands;
using OrderService.Library.Models;

namespace OrderService.API.Controllers.Administration
{
    public class OrderController : AdminOrderServiceController
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var res = await _mediator.Send(
                new AdmListOrderViewsQuery());

            return Ok(new QueryResponse<IEnumerable<OrderView>>(res));
        }

        [HttpPost("set-delivery-address")]
        public async Task<IActionResult> SetDeliveryAddress(
            [FromBody] SetOrderDeliveryAddressCommandDto command)
        {
            var res = await _mediator.Send(new AdmSetOrderDeliveryAddressCommand(
                command.OrderId,
                command.AddressLine,
                command.PostalCode,
                command.PostalOffice,
                command.Country
            ));

            return Ok(new CommandResponse<bool>(res));
        }

        [HttpPost("waiting-for-confirmation/{orderId}")]
        public async Task<IActionResult> SetWaitingForConfirmation(
            [FromRoute] Guid orderId)
        {
            var res = await _mediator.Send(
                new AdmSetOrderWaitForConfirmationCommand(orderId));

            return Ok(new CommandResponse<bool>(res));
        }

        [HttpPost("confirm/{orderId}")]
        public async Task<IActionResult> SetConfirm(
            [FromRoute] Guid orderId)
        {
            var res = await _mediator.Send(
                new AdmConfirmOrderCommand(orderId));

            return Ok(new CommandResponse<bool>(res));
        }
    }
}
