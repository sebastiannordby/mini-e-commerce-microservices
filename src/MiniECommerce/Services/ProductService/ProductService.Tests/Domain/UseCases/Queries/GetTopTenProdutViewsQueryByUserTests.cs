using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Authentication.Services;
using ProductService.Domain.Repositories;
using ProductService.Domain.UseCases.Queries.TopTenByUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Tests.Domain.UseCases.Queries
{
    public sealed class GetTopTenProdutViewsQueryByUserTests : BaseProductServiceTest
    {
        [Test]
        public async Task TestQuery()
        {
            var statsRepository = Services.GetRequiredService<IProductPurchaseStatsRepository>();
            var productRepository = Services.GetRequiredService<IProductRepository>();
            var currentUserService = Services.GetRequiredService<ICurrentUserService>();
            var mediator = Services.GetRequiredService<IMediator>();

            var topProductName = $"{nameof(TestQuery)}_1";
            var secondTopProductName = $"{nameof(TestQuery)}_2";

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

            var products = await mediator.Send(new GetTopTenProdutViewsQueryByUser());

            var productViewIndexZero = products.Count() >= 1 ?
                products.ElementAt(0) : null;
            var productViewIndexOne = products.Count() >= 2 ?
                products.ElementAt(1) : null;

            Assert.IsNotNull(products);
            Assert.IsNotEmpty(products);
            Assert.AreEqual(2, products.Count());
            Assert.IsNotNull(productViewIndexZero);
            Assert.IsNotNull(productViewIndexOne);
            Assert.AreEqual(productViewIndexZero.Id, topProductId);
            Assert.AreEqual(productViewIndexOne.Id, secondTopProductId);
        }

        [Test]
        public async Task TestReturnsProductWhenUserHasNotBought()
        {
            var statsRepository = Services.GetRequiredService<IProductPurchaseStatsRepository>();
            var productRepository = Services.GetRequiredService<IProductRepository>();
            var currentUserService = Services.GetRequiredService<ICurrentUserService>();
            var mediator = Services.GetRequiredService<IMediator>();

            var topProductName = $"{nameof(TestQuery)}_1";
            var secondTopProductName = $"{nameof(TestQuery)}_2";

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
                 "not@signed.on",
                 secondTopProductId,
                 quantity: 1);

            await statsRepository.InsertOrUpdateAsync(
                 "not@signed.on",
                 topProductId,
                 quantity: 2);

            var products = await mediator.Send(new GetTopTenProdutViewsQueryByUser());

            var productViewIndexZero = products.Count() >= 1 ?
                products.ElementAt(0) : null;
            var productViewIndexOne = products.Count() >= 2 ?
                products.ElementAt(1) : null;

            Assert.IsNotNull(products);
            Assert.IsNotEmpty(products);
            Assert.AreEqual(2, products.Count());
            Assert.IsNotNull(productViewIndexZero);
            Assert.IsNotNull(productViewIndexOne);
            Assert.AreEqual(productViewIndexZero.Id, topProductId);
            Assert.AreEqual(productViewIndexOne.Id, secondTopProductId);
        }
    }
}
