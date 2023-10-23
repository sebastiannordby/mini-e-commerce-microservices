using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Domain.UseCases.Commands.Start;
using OrderService.Library.Commands;
using MiniECommece.APIUtilities;
using MiniECommerce.Authentication.Services;
using OrderService.Domain.UseCases.Queries.FindView;

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
        public async Task<IActionResult> GetOrder(
            [FromRoute] Guid id)
        {
            var result = await _mediator.Send(
                new FindOrderViewByIdQuery(id));

            return Ok(result);
        }
            
        [HttpPost("start")]
        public async Task<IActionResult> StartOrder(
            [FromBody] StartOrderCommandDto commandDto)
        {
            var result = await _mediator.Send(new StartOrderCommand(
                Request.GetRequestId(),
                commandDto.BuyersFullName
            ));

            return Ok(result);
        }
    }
}