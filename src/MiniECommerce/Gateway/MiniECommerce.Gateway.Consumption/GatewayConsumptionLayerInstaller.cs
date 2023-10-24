using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
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
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                .AddScoped<AuthorizationHeaderService>()
                .AddScoped<IGatewayProductRepository, GatewayProductRepository>()
                .AddScoped<IGatewayBasketRepository, GatewayBasketRepository>();
        }
    }
}