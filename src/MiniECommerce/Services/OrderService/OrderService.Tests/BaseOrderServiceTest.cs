using MiniECommerce.Testing;
using OrderService.Domain;
using OrderService.DataAccess;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;
using MassTransit;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MiniECommerce.Library.Services.ProductService;
using MiniECommerce.Library.Services.BasketService;
using OrderService.Tests.Repositories;

namespace OrderService.Tests
{
    public class BaseOrderServiceTest
    {
        public IServiceProvider Services { get; private set; }

        [SetUp]
        public void Setup()
        {
            Services = ServiceProviderBuilder.BuildServiceProvider((services) =>
            {
                services.AddOrderServiceDomainLayer();
                services.AddOrderServiceDataAccessLayer(efOptions =>
                {
                    efOptions.UseInMemoryDatabase(nameof(BaseOrderServiceTest), b => {
                        b.EnableNullChecks(false);
                    });
                });
                services.RemoveAll<IGatewayBasketRepository>();
                services.AddScoped<IGatewayBasketRepository, GatewayMockBasketRepository>();
                services.AddMassTransitTestHarness(x =>
                {
                    x.AddConsumers(typeof(OrderService.Domain.Consumers.ProductAddedToBasketConsumer).Assembly);
                });
            });
        }

        [TearDown]
        public void Cleanup()
        {
            var dbContext = Services.GetService<OrderDbContext>();

            dbContext.Database.EnsureDeleted();
        }
    }
}