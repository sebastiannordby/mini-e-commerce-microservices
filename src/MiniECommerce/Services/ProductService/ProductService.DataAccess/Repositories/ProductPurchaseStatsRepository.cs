using Microsoft.EntityFrameworkCore;
using ProductService.DataAccess.Models;
using ProductService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.DataAccess.Repositories
{
    internal sealed class ProductPurchaseStatsRepository : IProductPurchaseStatsRepository
    {
        private readonly ProductDbContext _dbContext;

        public ProductPurchaseStatsRepository(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InsertOrUpdate(
            string buyersEmailAddress, Guid productId, int quantity)
        {
            var productExists = await _dbContext.Products
                .AnyAsync(x => x.Id == productId);
            if (!productExists)
                throw new Exception("No product with given id.");

            var updatedCount = await _dbContext.ProductPurchaseStats
                .Where(x => x.BuyersEmailAddress == buyersEmailAddress)
                .Where(x => x.ProductId == productId)
                .ExecuteUpdateAsync(props => props
                    .SetProperty(x => x.TotalQuantityBought, x => x.TotalQuantityBought + quantity)
                    .SetProperty(x => x.NumberOfTimesOrdered, x => x.NumberOfTimesOrdered + 1));
            if (updatedCount > 0)
                return;

            await _dbContext.ProductPurchaseStats.AddAsync(new ProductPurchaseStatsDao()
            {
                BuyersEmailAddress = buyersEmailAddress,
                ProductId = productId,
                NumberOfTimesOrdered = 1,
                TotalQuantityBought = quantity
            });

            await _dbContext.SaveChangesAsync();
        }
    }
}
