using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.Domain.UseCases.Queries.List;
using ProductService.Library.Models;
using MiniECommece.APIUtilities;
using ProductService.Domain.UseCases.Queries.Find;
using MiniECommerce.Library;
using System.Security.Claims;

namespace ProductService.API.Controllers
{
    public class ProductViewController : ProductServiceController
    {
        private readonly IMediator _mediator;

        public ProductViewController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductView>> ListViews(
            [FromQuery] decimal? fromPricePerQuantity = null,
            [FromQuery] decimal? toPricePerQuantity = null,
            [FromQuery] IEnumerable<string>? categories = null)
        {
            var res = await _mediator.Send(
                new ListProductViewsQuery(
                    fromPricePerQuantity,
                    toPricePerQuantity,
                    categories
                )
            );

            return res;
        }

        [HttpGet("id/{productId}")]
        public async Task<ProductView?> ListViews([FromRoute] Guid productId)
            => await _mediator.Send(new FindProductViewByIdQuery(productId));
    }
}
