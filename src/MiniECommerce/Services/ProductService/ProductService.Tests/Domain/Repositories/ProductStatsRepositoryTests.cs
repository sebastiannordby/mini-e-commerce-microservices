using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Authentication.Services;
using ProductService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Tests.Domain.Repositories
{
    public class ProductStatsRepositoryTests : BaseProductServiceTest
    {
        [Test]
        public async Task TestInsertStats()
        {
            var statsRepository = Services.GetRequiredService<IProductPurchaseStatsRepository>();
            var productRepository = Services.GetRequiredService<IProductRepository>();
            var currentUserService = Services.GetRequiredService<ICurrentUserService>();

            var productId = await productRepository.Create(new() { 
                Name = "",
                Category = nameof(TestInsertStats),
                Description = nameof(TestInsertStats),
                ImageUri = "https://dummydata.com/img_hello.png",
                Number = 1,
                PricePerQuantity = 22
            });

            Assert.DoesNotThrowAsync(async () =>
            {
                await statsRepository.InsertOrUpdateAsync(
                    currentUserService.UserEmail,
                    productId,
                    quantity: 1
                );
            });
        }
    }
}
