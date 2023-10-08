﻿using Microsoft.AspNetCore.Mvc;
using MiniECommerce.Gateway.Consumption.ProductService;
using ProductService.Library.Models;
using MiniECommece.APIUtilities;
using BasketService.Library;
using System.Threading.Tasks;
using System;

namespace BasketService.API.Controllers
{

    public class BasketController : BasketServiceController
    {
        private readonly IGatewayProductRepository _productRepository;
        private readonly List<BasketItemView> _items = new();

        public BasketController(
            IGatewayProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public IEnumerable<BasketItemView> GetList()
        {
            return _items;
        }

        [HttpPost("add/productid/{productId}")]
        public async Task AddToBasket([FromRoute] Guid productId)
        {
            var existsInBasket = _items
                .Any(x => x.ProductId == productId);
            if (existsInBasket)
                throw new Exception("Product already exists in basket.");

            var product = await _productRepository
                .Find(productId, Request.GetRequestId());
            if (product == null)
                throw new Exception("Product not found");

            _items.Add(new BasketItemView()
            {
                PricePerQuantity = product.PricePerQuantity,
                ProductId = productId,
                Quantity = 1
            });
        }

        [HttpPost("increase-quantity/{productId}")]
        public void IncreaseQuantity([FromRoute] Guid productId)
        {
            var product = _items
                .FirstOrDefault(x => x.ProductId == productId);
            if (product == null)
                throw new Exception("Product not in basket.");

            product.PricePerQuantity += 1;
        }

        [HttpPost("decrease-quantity/{productId}")]
        public void DecreaseQuantity([FromRoute] Guid productId)
        {
            var product = _items
                .FirstOrDefault(x => x.ProductId == productId);
            if (product == null)
                throw new Exception("Product not in basket.");

            product.PricePerQuantity -= 1;
            if (product.PricePerQuantity <= 0)
                _items.Remove(product);
        }
    }
}
