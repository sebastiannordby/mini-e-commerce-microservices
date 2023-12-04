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
using OrderService.Domain.Services;
using OrderService.Tests.Services;
using MiniECommerce.Library.Services.UserService;

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
                    efOptions.UseInMemoryDatabase(nameof(BaseOrderServiceTest), b =>
                    {
                        b.EnableNullChecks(false);
                    });
                });
                
                services.RemoveAll<IGatewayBasketRepository>();
                services.RemoveAll<IGatewayUserRepository>();

                services.RemoveAll<IOrderService>();
                services.RemoveAll<IUnitOfWork>();

                services.AddScoped<IUnitOfWork, MockUnitOfWork>();
                services.AddScoped<IOrderService, MockOrderService>();

                services.AddScoped<IGatewayBasketRepository, GatewayMockBasketRepository>();
                services.AddScoped<IGatewayUserRepository, GatewayMockUserRepository>();


                services.AddMassTransitTestHarness(x =>
                {
                    x.AddConsumers(typeof(OrderService.Domain.Consumers.ProductAddedToBasketConsumer).Assembly);
                });
            });
        }

        [TearDown]
        public void Cleanup()
        {
            var dbContext = Services.GetRequiredService<OrderDbContext>();

            dbContext.Database.EnsureDeleted();
        }
    }
}