
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiniECommerce.Library.Responses;
using OrderService.Domain.UseCases.Administration.Queries.ListOrders;
using OrderService.Library.Models;

namespace OrderService.API.Controllers.Administration
{
    public class AdminOrderController : AdminOrderServiceController
    {
        private readonly IMediator _mediator;

        public AdminOrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var res = await _mediator.Send(
                new ListOrderViewsQuery());

            return Ok(new QueryResponse<IEnumerable<OrderView>>(res));
        }
    }
}
