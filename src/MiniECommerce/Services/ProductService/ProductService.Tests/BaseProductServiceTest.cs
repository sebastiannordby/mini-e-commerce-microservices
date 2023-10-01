using MiniECommerce.Testing;
using ProductService.Domain;
using ProductService.DataAccess;

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
                services.AddProductDataAccessLayer();
            });
        }
    }
}