using MiniECommerce.Library.Services.ProductService;
using ProductService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Tests.Mocking.Repositories
{
    internal class GatewayMockProductRepository : IGatewayProductRepository
    {
        public Task<ProductView?> Find(Guid id)
        {
            return Task.FromResult<ProductView?>(new ProductView()
            {
                Id = id,
                Number = 1,
                Name = nameof(GatewayMockProductRepository),
                Category = nameof(GatewayMockProductRepository),
                Description = nameof(GatewayMockProductRepository),
                PricePerQuantity = 11
            });
        }
    }
}
