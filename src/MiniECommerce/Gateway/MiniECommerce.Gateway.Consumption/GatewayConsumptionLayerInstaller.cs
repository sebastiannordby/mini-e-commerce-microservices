using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Gateway.Consumption.BasketService;
using MiniECommerce.Gateway.Consumption.ProductService;

namespace MiniECommerce.Gateway.Consumption
{
    public static class GatewayConsumptionLayerInstaller
    {
        public static IServiceCollection AddGatewayConsumption(
            this IServiceCollection services)
        {
            return services
                .AddHttpClient()
                .AddScoped<IGatewayProductRepository, GatewayProductRepository>()
                .AddScoped<IGatewayBasketRepository, GatewayBasketRepository>();
        }
    }
}