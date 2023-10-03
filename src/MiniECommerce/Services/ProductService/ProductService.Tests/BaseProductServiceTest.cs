using MiniECommerce.Testing;
using ProductService.Domain;
using ProductService.DataAccess;
using Microsoft.EntityFrameworkCore;

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
                services.AddProductDataAccessLayer(efOptions =>
                {
                    efOptions.UseInMemoryDatabase(nameof(BaseProductServiceTest), b => {
                        b.EnableNullChecks(false);
                    });
                });
            });
        }
    }
}