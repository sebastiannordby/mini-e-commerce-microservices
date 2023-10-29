﻿using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Consumption.Repositories.OrderService;
using MiniECommerce.Consumption.Repositories.BasketService;
using MiniECommerce.Consumption.Repositories.ProductService;
using MiniECommerce.Consumption.Repositories.OrderService.Administration;

namespace MiniECommerce.Consumption
{
    public static class ConsumptionLayerInstaller
    {
        public static IServiceCollection AddConsumptionLayer(
            this IServiceCollection services)
        {
            return services
                .AddScoped<IProductRepository, ProductRepository>()
                .AddScoped<IBasketRepository, BasketRepository>()
                .AddScoped<IOrderRepository, OrderRepository>()
                .AddScoped<IOrderAdminRepository, OrderAdminRepository>();
        }
    }
}