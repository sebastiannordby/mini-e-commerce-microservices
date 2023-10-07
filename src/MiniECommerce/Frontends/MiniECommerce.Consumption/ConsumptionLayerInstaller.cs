using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Consumption.Repositories.ProductService;

namespace MiniECommerce.Consumption
{
    public static class ConsumptionLayerInstaller
    {
        public static IServiceCollection AddConsumptionLayer(
            this IServiceCollection services)
        {
            return services
                .AddScoped<IProductRepository, ProductRepository>();
        }
    }
}