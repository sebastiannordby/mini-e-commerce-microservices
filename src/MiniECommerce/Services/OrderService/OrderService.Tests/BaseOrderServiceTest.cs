using MiniECommerce.Testing;
using OrderService.Domain;
using OrderService.DataAccess;

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
                services.AddOrderServiceDataAccessLayer();
            });
        }
    }
}