using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Library.Commands;
using MiniECommece.APIUtilities;
using MiniECommerce.Authentication.Services;
using MiniECommerce.Library.Responses;
using OrderService.Library.Models;
using OrderService.Domain.UseCases.UserBased.Queries.FindStarted;
using OrderService.Domain.UseCases.UserBased.Queries.FindView;
using OrderService.Domain.UseCases.UserBased.Commands.Start;
using OrderService.Domain.UseCases.UserBased.Commands.SetAddress;

namespace OrderService.API.Controllers
{
    public class OrderController : OrderServiceController
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
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


        [HttpPost("set-address")]
        public async Task<IActionResult> SetAddress(
            [FromBody] SetOrderAddressCommandDto command)
        {
            if (command is null)
                return BadRequest();

            var result = await _mediator.Send(
                new SetOrderAddressCommand(
                    command.AddressLine,
                    command.PostalCode,
                    command.PostalOffice,
                    command.Country));

            return Ok(new CommandResponse<bool>(result));
        }

        [HttpPost("start")]
        public async Task<IActionResult> StartOrder(
            [FromBody] StartOrderCommandDto commandDto)
        {
            var result = await _mediator.Send(new StartOrderCommand(
                commandDto.BuyersFullName
            ));

            return Ok(new CommandResponse<Guid>(result));
        }
    }
}