using MiniECommerce.Testing;
using OrderService.Domain;
using OrderService.DataAccess;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;

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