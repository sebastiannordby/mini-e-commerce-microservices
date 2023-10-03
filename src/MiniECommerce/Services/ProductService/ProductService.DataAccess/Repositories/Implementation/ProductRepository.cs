﻿using Microsoft.EntityFrameworkCore;
using ProductService.DataAccess.Models;
using ProductService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.DataAccess.Repositories.Implementation
{
    internal sealed class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _dbContext;

        public ProductRepository(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProductDto> Find(Guid id)
        {
            var productDao = await _dbContext.Products
                .Select(x => ConvertToDto(x))
                .FirstOrDefaultAsync(x => x.Id == id);

            return productDao;
        }

        private ProductDto ConvertToDto(ProductDao x)
        {
            return new ProductDto(
                id: x.Id,
                number: x.Number,
                name: x.Name,
                description: x.Description,
                category: x.Category,
                pricePerQuantity: x.PricePerQuantity
            );
        }

        public async Task<Guid> Create(ProductDto product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            var productDao = new ProductDao(
                id: product.Id,
                number: product.Number,
                name: product.Name,
                description: product.Description,
                category: product.Category,
                pricePerQuantity: product.PricePerQuantity); 

            await _dbContext.Products.AddAsync( productDao );
            await _dbContext.SaveChangesAsync();

            return productDao.Id;
        }

        public async Task Update(ProductDto product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            var productDao = await _dbContext.Products
                .FirstOrDefaultAsync(x => x.Id == product.Id);
            if (productDao == null)
                throw new ArgumentException($"No Product with given Id({product.Id})");

            productDao.Update(
                product.Name,
                product.Description,
                product.Category,
                product.PricePerQuantity);

            _dbContext.Products.Update(productDao);
            await _dbContext.SaveChangesAsync();
        }
    }
}
