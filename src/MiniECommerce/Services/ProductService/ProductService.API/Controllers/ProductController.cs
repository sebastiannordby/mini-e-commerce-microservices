using Microsoft.AspNetCore.Mvc;
using ProductService.DataAccess.Repositories;
using ProductService.Library.Models;

namespace ProductService.API.Controllers
{
    public class ProductController : ProductServiceController
    {
        private readonly IProductViewRepository _productViewRepository;

        public ProductController(
            IProductViewRepository productViewRepository)
        {
            _productViewRepository = productViewRepository;
        }

        public async Task<IEnumerable<ProductView>> ListViews()
        {
            return await _productViewRepository.List();
        }
    }
}
