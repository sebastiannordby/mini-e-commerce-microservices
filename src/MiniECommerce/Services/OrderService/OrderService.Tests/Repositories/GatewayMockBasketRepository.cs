﻿using BasketService.Library;
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
        public async Task<IEnumerable<BasketItemView>?> GetList(string userEmail)
        {
            return await Task.FromResult(new List<BasketItemView>() 
            {
                new BasketItemView() 
                { 
                    ProductId = Guid.NewGuid(),
                    PricePerQuantity = 1,
                    ProductCategory = nameof(GatewayMockBasketRepository),
                    ProductDescription = nameof(GatewayMockBasketRepository),
                    ProductName = nameof(GatewayMockBasketRepository),
                    Quantity = 1
                } 
            }.AsEnumerable());
        }
    }
}
