using MiniECommerce.Testing;
using BasketService.Domain;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MiniECommerce.Gateway.Consumption.ProductService;
using MiniECommerce.Gateway.Consumption;
using Microsoft.Extensions.DependencyInjection;
using BasketService.Tests.Mocking.Repositories;

namespace BasketService.Tests
{
    public class BaseBasketServiceTest
    {
        protected IServiceProvider Services { get; private set; }

        [SetUp]
        public void Setup()
        {
            Services = ServiceProviderBuilder.BuildServiceProvider((services) =>
            {
                services.AddGatewayConsumption();
                services.AddBasketServiceDomainLayer();
                services.RemoveAll<IGatewayProductRepository>();
                services.AddScoped<IGatewayProductRepository, GatewayMockProductRepository>();
            });
        }
    }
}