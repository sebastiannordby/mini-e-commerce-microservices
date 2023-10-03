using ProductService.DataAccess.Models;
using ProductService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProductService.DataAccess.Repositories.Implementation
{
    internal sealed class ProductViewRepository : IProductViewRepository
    {
        private readonly ProductDbContext _dbContext;

        public ProductViewRepository(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ProductView>> List()
        {
            return await _dbContext.Products
                .AsNoTracking()
                .Select(x => ConvertToView(x))
                .ToListAsync();
        }

        private ProductView ConvertToView(ProductDao dao)
        {
            return new ProductView(
                id: dao.Id,
                number: dao.Number,
                name: dao.Name,
                description: dao.Description,
                category: dao.Category,
                pricePerQuantity: dao.PricePerQuantity
            );
        }
    }
}
