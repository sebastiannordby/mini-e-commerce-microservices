using BasketService.Library;
using MiniECommerce.Library.Services.BasketService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Tests.Repositories
{
    internal class GatewayMockBasketRepository : IGatewayBasketRepository
    {
        public async Task<IEnumerable<BasketItemView>?> GetList(Guid requestId, string userEmail)
        {
            return await Task.FromResult(new List<BasketItemView>() 
            {
                new BasketItemView() 
                { 
                    ProductId = Guid.NewGuid(),
                    PricePerQuantity = 1,
                    ProductName = nameof(GatewayMockBasketRepository),
                    Quantity = 1
                } 
            }.AsEnumerable());
        }
    }
}
