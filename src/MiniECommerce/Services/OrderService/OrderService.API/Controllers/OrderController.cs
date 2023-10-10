using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Domain.UseCases.Commands.Start;
using OrderService.Library.Commands;
using MiniECommece.APIUtilities;

namespace OrderService.API.Controllers
{
    public class OrderController : OrderServiceController
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("start")]
        public async Task<IActionResult> StartOrder(
            [FromBody] StartOrderCommandDto commandDto)
        {
            var result = await _mediator.Send(new StartOrderCommand(
                Request.GetRequestId(),
                commandDto.BasketId,
                commandDto.BuyersFullName,
                commandDto.BuyersEmailAddress
            ));

            return Ok(result);
        }
    }
}