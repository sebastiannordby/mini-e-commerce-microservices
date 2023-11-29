using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Library.Commands;
using MiniECommece.APIUtilities;
using MiniECommerce.Authentication.Services;
using MiniECommerce.Library.Responses;
using OrderService.Library.Models;
using OrderService.Domain.UseCases.CustomerBased.Queries.FindStarted;
using OrderService.Domain.UseCases.CustomerBased.Queries.FindView;
using OrderService.Domain.UseCases.CustomerBased.Commands.Start;
using OrderService.Domain.UseCases.CustomerBased.Commands.SetDeliveryAddress;
using OrderService.Domain.UseCases.CustomerBased.Queries.ListViews;
using FluentResults;
using OrderService.Domain.UseCases.CustomerBased.Commands.SetInvoiceAddress;

namespace OrderService.API.Controllers.CustomerBased
{
    public class OrderController : OrderServiceController
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var result = await _mediator.Send(
                new ListOrderViewsQuery());

            return Ok(new QueryResponse<IEnumerable<OrderView>>(result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder([FromRoute] Guid id)
        {
            var result = await _mediator.Send(
                new FindOrderViewByIdQuery(id));

            return Ok(new QueryResponse<OrderView?>(result));
        }

        [HttpGet("started-order")]
        public async Task<IActionResult> GetStartedOrder()
        {
            var result = await _mediator.Send(
                new FindStartedOrderQuery());

            return Ok(new QueryResponse<Guid?>(result));
        }

        [HttpPost("set-delivery-address")]
        public async Task<IActionResult> SetDeliveryAddress(
            [FromBody] SetOrderDeliveryAddressCommandDto command)
        {
            if (command is null)
                return BadRequest();

            var result = await _mediator.Send(
                new SetOrderDeliveryAddressCommand(
                    command.AddressLine,
                    command.PostalCode,
                    command.PostalOffice,
                    command.Country));

            return Ok(new CommandResponse<Result>(result));
        }

        [HttpPost("set-delivery-address")]
        public async Task<IActionResult> SetInvoiceAddress(
            [FromBody] SetOrderInvoiceAddressCommandDto command)
        {
            if (command is null)
                return BadRequest();

            var result = await _mediator.Send(
                new SetOrderInvoiceAddressCommand(
                    command.AddressLine,
                    command.PostalCode,
                    command.PostalOffice,
                    command.Country));

            return Ok(new CommandResponse<Result>(result));
        }

        [HttpPost("start")]
        public async Task<IActionResult> StartOrder()
        {
            var result = await _mediator.Send(
                new StartOrderCommand());

            return Ok(new CommandResponse<Guid>(result));
        }
    }
}