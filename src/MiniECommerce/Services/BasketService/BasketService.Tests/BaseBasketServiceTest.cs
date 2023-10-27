using MiniECommerce.Testing;
using BasketService.Domain;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using BasketService.Tests.Mocking.Repositories;
using MiniECommerce.Library.Services.ProductService;
using MassTransit;

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
                services.AddBasketServiceDomainLayer();
                services.RemoveAll<IGatewayProductRepository>();
                services.AddScoped<IGatewayProductRepository, GatewayMockProductRepository>();
                services.AddMassTransitTestHarness(x =>
                {

                });
            });
        }
    }
}