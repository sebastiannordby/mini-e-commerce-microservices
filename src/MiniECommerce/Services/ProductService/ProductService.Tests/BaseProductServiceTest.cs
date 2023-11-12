using MiniECommerce.Testing;
using Microsoft.EntityFrameworkCore;
using ProductService.DataAccess;
using ProductService.Domain;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ProductService.Domain.Repositories;
using ProductService.Tests.Mock.Repositories;

namespace ProductService.Tests
{
    public class BaseProductServiceTest
    {
        public IServiceProvider Services { get; private set; }

        [SetUp]
        public void Setup()
        {
            Services = ServiceProviderBuilder.BuildServiceProvider((services) =>
            {
                services.AddProductServiceDomainLayer();
                services.AddProductServiceDataAccessLayer(efOptions =>
                {
                    efOptions.UseInMemoryDatabase(nameof(BaseProductServiceTest), b => {
                        b.EnableNullChecks(false);
                    });
                });

                services.RemoveAll<IProductPurchaseStatsRepository>();
                services.AddScoped<IProductPurchaseStatsRepository, MockProductPurchaseStatsRepository>();

                services.AddMassTransitTestHarness(x =>
                {
                    x.AddConsumers(typeof(ProductService.Domain.Consumers.OrderSetToWaitingForConfirmationConsumer).Assembly);
                });
            });
        }

        [TearDown]
        public void Cleanup()
        {
            var dbContext = Services
                .GetRequiredService<ProductDbContext>();

            dbContext.Database.EnsureDeleted();
        }
    }
}