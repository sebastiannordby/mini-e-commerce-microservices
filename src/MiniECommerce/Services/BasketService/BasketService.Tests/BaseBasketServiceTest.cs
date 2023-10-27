using MassTransit;
using BasketService.Domain;
using MiniECommerce.Testing;
using Microsoft.Extensions.DependencyInjection;
using BasketService.Tests.Mocking.Repositories;
using MiniECommerce.Library.Services.ProductService;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MiniECommerce.Library.Events.OrderService;
using BasketService.Domain.Consumers;

namespace BasketService.Tests
{
    public class BaseBasketServiceTest
    {
        protected IServiceProvider Services { get; private set; }

        [SetUp]
        public void Setup()
        {
            Services = GetServiceProvider();
        }

        private IServiceProvider GetServiceProvider()
        {
            return ServiceProviderBuilder.BuildServiceProvider((services) =>
            {
                services.AddBasketServiceDomainLayer();
                services.RemoveAll<IGatewayProductRepository>();
                services.AddScoped<IGatewayProductRepository, GatewayMockProductRepository>();
                services.AddMassTransitTestHarness(x =>
                {
                    x.AddConsumers(typeof(BasketService.Domain.Consumers.OrderStartedEventConsumer).Assembly);
                });
            });
        }
    }
}