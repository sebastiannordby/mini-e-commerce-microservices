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
using ProductService.Domain.UseCases.Queries.ListCategories;

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
        {
            var response = await _mediator.Send(
                new FindProductByIdQuery(id));

            return response;
        }

        [HttpGet("categories")]
        public async Task<IEnumerable<string>> ListCategories()
        {
            var response = await _mediator.Send(
                new ListProductCategoriesQuery());

            return response;
        }

        [HttpPost]
        public async Task<Guid> Create([FromBody] ProductDto product)
        {
            var response = await _mediator.Send(
                new CreateProductCommand(product));

            return response;
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductDto product)
        {
            await _mediator.Send(
                new UpdateProductCommand(product));

            return Ok();
        }
    }
}
