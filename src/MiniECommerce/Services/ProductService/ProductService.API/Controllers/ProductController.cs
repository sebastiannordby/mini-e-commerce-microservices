using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.DataAccess.Repositories;
using ProductService.Domain.UseCases.Queries.ListProducts;
using ProductService.Library.Models;
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

        [HttpGet]
        public async Task<IEnumerable<ProductView>> ListViews()
            => await _mediator.Send(new ListProductQuery(Request.GetRequestId()));

        [HttpGet("id/{id}")]
        public async Task<ProductDto> Find(
            [FromRoute] Guid id)
        {
            return await _productRepository.Find(id);
        }

        [HttpPost]
        public async Task<Guid> Create(
            [FromBody] ProductDto product)
        {
            return await _productRepository.Create(product);
        }

        [HttpPut]
        public async Task<IActionResult> Update(
            [FromBody] ProductDto product)
        {
            await _productRepository.Update(product);

            return Ok();
        }
    }
}
