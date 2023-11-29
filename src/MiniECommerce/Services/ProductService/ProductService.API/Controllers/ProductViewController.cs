using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.Domain.UseCases.Queries.List;
using ProductService.Library.Models;
using MiniECommece.APIUtilities;
using ProductService.Domain.UseCases.Queries.Find;
using MiniECommerce.Library;
using System.Security.Claims;
using ProductService.Domain.UseCases.Queries.TopTenByUser;
using Prometheus;
using System.Diagnostics.Metrics;
using System.Diagnostics;

namespace ProductService.API.Controllers
{
    public class ProductViewController : ProductServiceController
    {
        private readonly IMediator _mediator;
        public static readonly Histogram _fetchProductHistogram = Metrics.CreateHistogram(
            "fetching_products",
            "Time it takes to list products.");


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
            var stopwatch = Stopwatch.StartNew();

            try
            {
                var res = await _mediator.Send(
                    new ListProductViewsQuery(
                        fromPricePerQuantity,
                        toPricePerQuantity,
                        categories));

                return res;
            }
            finally
            {
                stopwatch.Stop();
                _fetchProductHistogram.Observe(
                    stopwatch.Elapsed.TotalSeconds);
            }
        }

        [HttpGet("id/{productId}")]
        public async Task<ProductView?> ListViews([FromRoute] Guid productId)
            => await _mediator.Send(new FindProductViewByIdQuery(productId));

        [HttpGet("top-ten")]
        public async Task<IEnumerable<ProductView>> TopTen()
            => await _mediator.Send(new GetTopTenProdutViewsQueryByUser());

    }
}
