using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Domain.UseCases.Commands.Start;
using OrderService.Library.Commands;
using MiniECommece.APIUtilities;
using MiniECommerce.Authentication.Services;
using OrderService.Domain.UseCases.Queries.FindView;
using OrderService.Domain.UseCases.Queries.FindStarted;

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

            return Ok(result);
        }

        [HttpGet("started-order")]
        public async Task<IActionResult> GetStartedOrder()
        {
            var result = await _mediator.Send(
                new FindStartedOrderQuery());

            return Ok(result);
        }


        [HttpPost("start")]
        public async Task<IActionResult> StartOrder(
            [FromBody] StartOrderCommandDto commandDto)
        {
            var result = await _mediator.Send(new StartOrderCommand(
                commandDto.BuyersFullName
            ));

            return Ok(result);
        }
    }
}