using ProductService.DataAccess.Models;
using ProductService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Repositories;

namespace ProductService.DataAccess.Repositories
{
    internal sealed class ProductViewRepository : IProductViewRepository
    {
        private readonly ProductDbContext _dbContext;

        public ProductViewRepository(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProductView?> Find(Guid productId)
        {
            return await _dbContext.Products
                .AsNoTracking()
                .Where(x => x.Id == productId)
                .Select(x => ConvertToView(x))
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ProductView>> List(
            decimal? fromPricePerQuantity,
            decimal? toPricePerQuantity,
            IEnumerable<string>? categories)
        {
            var query = _dbContext.Products.AsNoTracking();

            if (fromPricePerQuantity.HasValue)
                query = query.Where(x =>
                    x.PricePerQuantity >= fromPricePerQuantity.Value);

            if (toPricePerQuantity.HasValue)
                query = query.Where(x =>
                    x.PricePerQuantity <= toPricePerQuantity.Value);

            if (categories?.Any() == true)
                query = query.Where(x =>
                    categories.Contains(x.Category));

            return await query
                .Select(x => ConvertToView(x))
                .ToListAsync();
        }

        public static ProductView ConvertToView(ProductDao dao)
        {
            return new ProductView(
                id: dao.Id,
                number: dao.Number,
                name: dao.Name,
                description: dao.Description,
                category: dao.Category,
                pricePerQuantity: dao.PricePerQuantity,
                imageUri: dao.ImageUri
            );
        }
    }
}
