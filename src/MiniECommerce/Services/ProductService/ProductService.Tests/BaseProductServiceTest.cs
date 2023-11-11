using MiniECommerce.Testing;
using Microsoft.EntityFrameworkCore;
using ProductService.DataAccess;
using ProductService.Domain;
using MassTransit;

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
                services.AddMassTransitTestHarness(x =>
                {
                    x.AddConsumers(typeof(ProductService.Domain.Consumers.OrderSetToWaitingForConfirmationConsumer).Assembly);
                });
            });
        }
    }
}