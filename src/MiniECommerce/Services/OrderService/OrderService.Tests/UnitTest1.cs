using MiniECommerce.Testing;
using OrderService.DataAccess;
using OrderService.Domain;

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
                services.AddOrderServiceDataAccessLayer();
                services.AddOrderServiceDomainLayer();
            });
        }
    }
}