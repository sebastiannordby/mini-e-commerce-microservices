using Microsoft.EntityFrameworkCore;
using ProductService.DataAccess.Models;
using ProductService.Domain.Repositories;
using ProductService.Library.Models;
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

        public async Task<IEnumerable<ProductView>> GetTopTenProducts()
        {
            var topProductsQuery = (
                from stats in _dbContext.ProductPurchaseStats
                    .AsNoTracking()
                    .GroupBy(x => x.ProductId)
                    .Select(grouping => new
                    {
                        ProductId = grouping.Key,
                        SumQuantityBought = grouping.Sum(p => p.TotalQuantityBought)
                    })
                join product in _dbContext.Products
                    on stats.ProductId equals product.Id
                orderby stats.SumQuantityBought descending
                select product
            );

            var topTenProductsQuery = topProductsQuery
                .Take(10)
                .Select(x => ProductViewRepository.ConvertToView(x));

            return await Task.FromResult(topTenProductsQuery);
        }

        public async Task<IEnumerable<ProductView>> GetTopTenProductsByUser(string userEmail)
        {
            var topProductsQuery = (
                from stats in _dbContext.ProductPurchaseStats
                    .AsNoTracking()
                    .Where(x => x.BuyersEmailAddress == userEmail)
                    .GroupBy(x => x.ProductId)
                    .Select(grouping => new
                    {
                        ProductId = grouping.Key,
                        SumQuantityBought = grouping.Sum(p => p.TotalQuantityBought)
                    })
                join product in _dbContext.Products
                    on stats.ProductId equals product.Id
                orderby stats.SumQuantityBought descending
                select product
            );

            var topTenProductsQuery = topProductsQuery
                .Take(10)
                .Select(x => ProductViewRepository.ConvertToView(x));

            return await Task.FromResult(topTenProductsQuery);
        }

        public async Task InsertOrUpdateAsync(
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
