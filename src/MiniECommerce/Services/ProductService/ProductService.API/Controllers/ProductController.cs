using Microsoft.AspNetCore.Mvc;
using ProductService.DataAccess.Repositories;
using ProductService.Library.Models;

namespace ProductService.API.Controllers
{
    public class ProductController : ProductServiceController
    {
        private readonly IProductViewRepository _productViewRepository;
        private readonly IProductRepository _productRepository;

        public ProductController(
            IProductViewRepository productViewRepository, 
            IProductRepository productRepository)
        {
            _productViewRepository = productViewRepository;
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductView>> ListViews()
        {
            return await _productViewRepository.List();
        }

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
