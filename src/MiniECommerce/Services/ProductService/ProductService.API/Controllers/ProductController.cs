using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.DataAccess.Repositories;
using ProductService.Domain.UseCases.Queries.List;
using ProductService.Library.Models;
using ProductService.Domain.UseCases.Commands.CreateProduct;
using ProductService.Domain.UseCases.Commands.UpdateProduct;
using ProductService.Domain.UseCases.Queries.Find;
using MiniECommece.APIUtilities;

namespace ProductService.API.Controllers
{
    public class ProductController : ProductServiceController
    {
        private readonly IProductViewRepository _productViewRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMediator _mediator;

        public ProductController(
            IProductViewRepository productViewRepository,
            IProductRepository productRepository,
            IMediator mediator)
        {
            _productViewRepository = productViewRepository;
            _productRepository = productRepository;
            _mediator = mediator;
        }

        [HttpGet("id/{id}")]
        public async Task<ProductDto?> Find([FromRoute] Guid id)
            => await _mediator.Send(
                new FindProductByIdQuery(Request.GetRequestId(), id));

        [HttpPost]
        public async Task<Guid> Create([FromBody] ProductDto product)
            => await _mediator.Send(
                new CreateProductCommand(Request.GetRequestId(), product));

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductDto product)
        {
            await _mediator.Send(
                new UpdateProductCommand(Request.GetRequestId(), product));

            return Ok();
        }
    }
}
