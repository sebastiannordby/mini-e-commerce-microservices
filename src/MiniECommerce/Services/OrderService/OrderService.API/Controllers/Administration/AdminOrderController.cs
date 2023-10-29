
using MediatR;

namespace OrderService.API.Controllers.Administration
{
    public class AdminOrderController : AdminOrderServiceController
    {
        private readonly IMediator _mediator;

        public AdminOrderController(IMediator mediator)
        {
            _mediator = mediator;
        }


    }
}
