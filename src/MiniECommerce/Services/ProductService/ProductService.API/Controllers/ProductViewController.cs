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
using System.Web;

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
            [FromQuery] string? search,
            [FromQuery] decimal? fromPricePerQuantity = null,
            [FromQuery] decimal? toPricePerQuantity = null,
            [FromQuery] IEnumerable<string>? categories = null)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                var categoriesDecoded = categories
                    ?.Select(x => HttpUtility.UrlDecode(x))
                    ?.ToList();

                var res = await _mediator.Send(
                    new ListProductViewsQuery(
                        search,
                        fromPricePerQuantity,
                        toPricePerQuantity,
                        categoriesDecoded));

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
