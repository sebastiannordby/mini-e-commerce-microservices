using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.Domain.UseCases.Queries.List;
using ProductService.Library.Models;
using MiniECommece.APIUtilities;
using ProductService.Domain.UseCases.Queries.Find;

namespace ProductService.API.Controllers
{
    public class ProductViewController : ProductServiceController
    {
        private readonly IMediator _mediator;

        public ProductViewController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductView>> ListViews()
            => await _mediator.Send(
                new ListProductViewsQuery(Request.GetRequestId()));

        [HttpGet("id/{productId}")]
        public async Task<ProductView?> ListViews([FromRoute] Guid productId)
            => await _mediator.Send(
                new FindProductViewByIdQuery(productId, Request.GetRequestId()));

    }
}
