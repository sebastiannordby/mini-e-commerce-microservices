﻿using Microsoft.EntityFrameworkCore;
using NSubstitute.ReceivedExtensions;
using ProductService.DataAccess;
using ProductService.DataAccess.Models;
using ProductService.DataAccess.Repositories;
using ProductService.Domain.Repositories;
using ProductService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Tests.Mock.Repositories
{
    public class MockProductPurchaseStatsRepository : IProductPurchaseStatsRepository
    {
        private readonly ProductDbContext _dbContext;
        private readonly IProductPurchaseStatsRepository _repository;

        public MockProductPurchaseStatsRepository(DbContextOptions dbContextOptions)
        {
            _dbContext = new(dbContextOptions);
            _repository = new ProductPurchaseStatsRepository(_dbContext);
        }

        public async Task<List<ProductView>> GetTopTenProducts()
        {
            return await _repository.GetTopTenProducts();
        }

        public async Task<List<ProductView>> GetTopTenProductsByUser(string userEmail)
        {
            return await _repository.GetTopTenProductsByUser(userEmail);
        }

        public async Task InsertOrUpdateAsync(
            string buyersEmailAddress, Guid productId, int quantity)
        {
            var productExists = await _dbContext.Products
                .AnyAsync(x => x.Id == productId);
            if (!productExists)
                throw new Exception("No product with given id.");

            var existingStats = await _dbContext.ProductPurchaseStats
                .Where(x => x.BuyersEmailAddress == buyersEmailAddress)
                .Where(x => x.ProductId == productId)
                .FirstOrDefaultAsync();
            if(existingStats is not null)
            {
                existingStats.TotalQuantityBought += quantity;
                existingStats.NumberOfTimesOrdered += 1;
                
                _dbContext.ProductPurchaseStats.Update(existingStats);
                await _dbContext.SaveChangesAsync();
                
                return;
            }

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
