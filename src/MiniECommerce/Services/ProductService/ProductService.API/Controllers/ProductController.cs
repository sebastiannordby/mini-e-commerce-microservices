using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.DataAccess.Repositories;
using ProductService.Domain.UseCases.Queries.List;
using ProductService.Library.Models;
using ProductService.Domain.UseCases.Commands.CreateProduct;
using ProductService.Domain.UseCases.Commands.UpdateProduct;
using ProductService.Domain.UseCases.Queries.Find;
using MiniECommece.APIUtilities;
using ProductService.Domain.Repositories;

namespace ProductService.API.Controllers
{
    public class ProductController : ProductServiceController
    {
        private readonly IMediator _mediator;

        public ProductController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("id/{id}")]
        public async Task<ProductDto?> Find([FromRoute] Guid id)
            => await _mediator.Send(
                new FindProductByIdQuery(id));

        [HttpPost]
        public async Task<Guid> Create([FromBody] ProductDto product)
            => await _mediator.Send(
                new CreateProductCommand(product));

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductDto product)
        {
            await _mediator.Send(
                new UpdateProductCommand(product));

            return Ok();
        }
    }
}
