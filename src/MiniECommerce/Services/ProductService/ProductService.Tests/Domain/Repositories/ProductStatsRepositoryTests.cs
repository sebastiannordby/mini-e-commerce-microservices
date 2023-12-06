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

        [Test]
        public async Task TestSelectTopTenProductsByUser()
        {
            var statsRepository = Services.GetRequiredService<IProductPurchaseStatsRepository>();
            var productRepository = Services.GetRequiredService<IProductRepository>();
            var currentUserService = Services.GetRequiredService<ICurrentUserService>();

            var topProductName = $"{nameof(TestInsertStats)}_1";
            var secondTopProductName = $"{nameof(TestInsertStats)}_2";

            var topProductId = await productRepository.Create(new()
            {
                Name = "",
                Category = topProductName,
                Description = topProductName,
                ImageUri = "https://dummydata.com/img_hello.png",
                Number = 1,
                PricePerQuantity = 22
            });

            var secondTopProductId = await productRepository.Create(new()
            {
                Name = "",
                Category = secondTopProductName,
                Description = secondTopProductName,
                ImageUri = "https://dummydata.com/img_hello.png",
                Number = 1,
                PricePerQuantity = 22
            });

            await statsRepository.InsertOrUpdateAsync(
                 currentUserService.UserEmail,
                 secondTopProductId,
                 quantity: 1);

            await statsRepository.InsertOrUpdateAsync(
                 currentUserService.UserEmail,
                 topProductId,
                 quantity: 2);

            var products = await statsRepository
                .GetTopTenProductsByUser(currentUserService.UserEmail);

            var productViewIndexZero = products.Count() >= 1 ? 
                products.ElementAt(0) : null;
            var productViewIndexOne = products.Count() >= 2 ? 
                products.ElementAt(1) : null;

            Assert.That(products, Is.Not.Null);
            Assert.That(products, Is.Not.Empty);
            Assert.That(2, Is.EqualTo(products.Count()));
            Assert.That(productViewIndexZero, Is.Not.Null);
            Assert.That(productViewIndexOne, Is.Not.Null);
            Assert.That(productViewIndexZero.Id, Is.EqualTo(topProductId));
            Assert.That(productViewIndexOne.Id, Is.EqualTo(secondTopProductId));
        }

        [Test]
        public async Task TestSelectTopTenProducts()
        {
            var statsRepository = Services.GetRequiredService<IProductPurchaseStatsRepository>();
            var productRepository = Services.GetRequiredService<IProductRepository>();
            var currentUserService = Services.GetRequiredService<ICurrentUserService>();

            var topProductName = $"{nameof(TestInsertStats)}_1";
            var secondTopProductName = $"{nameof(TestInsertStats)}_2";

            var topProductId = await productRepository.Create(new()
            {
                Name = "",
                Category = topProductName,
                Description = topProductName,
                ImageUri = "https://dummydata.com/img_hello.png",
                Number = 1,
                PricePerQuantity = 22
            });

            var secondTopProductId = await productRepository.Create(new()
            {
                Name = "",
                Category = secondTopProductName,
                Description = secondTopProductName,
                ImageUri = "https://dummydata.com/img_hello.png",
                Number = 1,
                PricePerQuantity = 22
            });

            await statsRepository.InsertOrUpdateAsync(
                 currentUserService.UserEmail + "1",
                 topProductId,
                 quantity: 122);

            await statsRepository.InsertOrUpdateAsync(
                 currentUserService.UserEmail + "2",
                 topProductId,
                 quantity: 2);

            await statsRepository.InsertOrUpdateAsync(
                 currentUserService.UserEmail + "1",
                 secondTopProductId,
                 quantity: 12);

            await statsRepository.InsertOrUpdateAsync(
                 currentUserService.UserEmail + "2",
                 secondTopProductId,
                 quantity: 10);

            var products = await statsRepository.GetTopTenProducts();
            var productViewIndexZero = products.Count() >= 1 ?
                products.ElementAt(0) : null;
            var productViewIndexOne = products.Count() >= 2 ?
                products.ElementAt(1) : null;

            Assert.That(products, Is.Not.Null);
            Assert.That(products, Is.Not.Empty);
            Assert.That(2, Is.EqualTo(products.Count()));
            Assert.That(productViewIndexZero, Is.Not.Null);
            Assert.That(productViewIndexOne, Is.Not.Null);
            Assert.That(productViewIndexZero.Id, Is.EqualTo(topProductId));
            Assert.That(productViewIndexOne.Id, Is.EqualTo(secondTopProductId));
        }
    }
}
