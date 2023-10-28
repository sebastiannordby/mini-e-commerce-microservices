using BasketService.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Consumption.Repositories.BasketService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Tests
{
    public class UserBasketServiceTests : BaseBasketServiceTest
    {
        [Test]
        public async Task BasketReturnsEmptyListNotNull()
        {
            var userBasketService = Services.GetRequiredService<IUserBasketService>();

            var basketItems = await userBasketService.GetBasket(
                nameof(UserBasketServiceTests));

            Assert.IsNotNull(basketItems);
            Assert.IsEmpty(basketItems);
        }

        [Test]
        public async Task AddsItemToBasket()
        {
            var userBasketService = Services.GetRequiredService<IUserBasketService>();

            var basketWithOneItem = (
                await userBasketService.AddToBasket(
                    Guid.NewGuid(), nameof(UserBasketServiceTests), Guid.NewGuid())).ToArray();

            var basketWithTwoItems = (
                await userBasketService.AddToBasket(
                    Guid.NewGuid(), nameof(UserBasketServiceTests), Guid.NewGuid())).ToArray();

            var basketItems = (await userBasketService.GetBasket(
                nameof(UserBasketServiceTests))).ToArray();

            await userBasketService.ClearBasket(nameof(UserBasketServiceTests));

            Assert.IsNotNull(basketWithOneItem);
            Assert.IsNotNull(basketWithTwoItems);
            Assert.IsNotNull(basketItems);
            Assert.IsTrue(basketWithOneItem.Length == 1);
            Assert.IsTrue(basketWithTwoItems.Length == 2);
            Assert.IsTrue(basketItems.Length == 2);
        }

        [Test]
        public async Task TestClearingBasket()
        {
            var userBasketService = Services.GetRequiredService<IUserBasketService>();

            var basketWithOneItem = (
                await userBasketService.AddToBasket(
                    Guid.NewGuid(), nameof(UserBasketServiceTests), Guid.NewGuid())).ToArray();

            var basketWithTwoItems = (
                await userBasketService.AddToBasket(
                    Guid.NewGuid(), nameof(UserBasketServiceTests), Guid.NewGuid())).ToArray();

            var basketItemsBeforeClear = (await userBasketService.GetBasket(
                nameof(UserBasketServiceTests))).ToArray();

            await userBasketService.ClearBasket(nameof(UserBasketServiceTests));

            var basketItemsAfterClear = (await userBasketService.GetBasket(
                nameof(UserBasketServiceTests))).ToArray();

            await userBasketService.ClearBasket(nameof(UserBasketServiceTests));

            Assert.IsNotNull(basketWithOneItem);
            Assert.IsTrue(basketWithOneItem.Length == 1);

            Assert.IsNotNull(basketWithTwoItems);
            Assert.IsTrue(basketWithTwoItems.Length == 2);

            Assert.IsNotNull(basketItemsBeforeClear);
            Assert.IsTrue(basketItemsBeforeClear.Length == 2);

            Assert.IsNotNull(basketItemsAfterClear);
            Assert.IsTrue(basketItemsAfterClear.Length == 0);
        }

        [Test]
        public async Task TestIncreaseQuantity()
        {
            var userBasketService = Services.GetRequiredService<IUserBasketService>();

            var productOneId = Guid.NewGuid();

            var initialBasket = (
                await userBasketService.AddToBasket(
                    Guid.NewGuid(), nameof(UserBasketServiceTests), productOneId)).ToArray();

            var initialProductQuantity = initialBasket
                .Where(x => x.ProductId == productOneId)
                .Select(x => x.Quantity)
                .First();

            var increasedBasket = await userBasketService.IncreaseQuantity(
                nameof(UserBasketServiceTests), productOneId);

            var increasedProductQuantity = initialBasket
                .Where(x => x.ProductId == productOneId)
                .Select(x => x.Quantity)
                .First();

            await userBasketService.ClearBasket(nameof(UserBasketServiceTests));

            Assert.IsNotNull(initialBasket);
            Assert.IsTrue(initialProductQuantity == 1);
            Assert.That(initialBasket.Length == 1);

            Assert.IsNotNull(increasedBasket);
            Assert.That(increasedBasket.Count == 1);
            Assert.IsTrue(increasedProductQuantity == 2);
        }

        [Test]
        public async Task TestDecreaseQuantity()
        {
            var userBasketService = Services.GetRequiredService<IUserBasketService>();

            var productOneId = Guid.NewGuid();

            var initialBasket = (
                await userBasketService.AddToBasket(
                    Guid.NewGuid(), nameof(UserBasketServiceTests), productOneId)).ToArray();

            var initialProductQuantity = initialBasket
                .Where(x => x.ProductId == productOneId)
                .Select(x => x.Quantity)
                .First();

            var increasedBasket = (
                await userBasketService.IncreaseQuantity(
                    nameof(UserBasketServiceTests), productOneId)).ToArray();

            var increasedProductQuantity = initialBasket
                .Where(x => x.ProductId == productOneId)
                .Select(x => x.Quantity)
                .First();

            var decrearedBasket = (
                await userBasketService.DecreaseQuantity(
                    nameof(UserBasketServiceTests), productOneId)).ToArray();

            var decrearedBasketProductCount = initialBasket
                .Where(x => x.ProductId == productOneId)
                .Select(x => x.Quantity)
                .First();

            await userBasketService.ClearBasket(nameof(UserBasketServiceTests));

            Assert.IsNotNull(initialBasket);
            Assert.IsTrue(initialProductQuantity == 1);
            Assert.That(initialBasket.Length == 1);
           
            Assert.IsNotNull(increasedBasket);
            Assert.That(increasedBasket.Length == 1);
            Assert.IsTrue(increasedProductQuantity == 2);
            
            Assert.That(decrearedBasketProductCount == 1);
            Assert.That(decrearedBasket.Length == 1);
        }

        [Test]
        public async Task TestDecreaseQuantityShouldRemoveItem()
        {
            var userBasketService = Services.GetRequiredService<IUserBasketService>();

            var productOneId = Guid.NewGuid();

            var initialBasket = (
                await userBasketService.AddToBasket(
                    Guid.NewGuid(), nameof(UserBasketServiceTests), productOneId)).ToArray();

            var initialProductQuantity = initialBasket
                .Where(x => x.ProductId == productOneId)
                .Select(x => x.Quantity)
                .First();

            var decreasedBasket = (
                await userBasketService.DecreaseQuantity(
                    nameof(UserBasketServiceTests), productOneId)).ToArray();

            await userBasketService.ClearBasket(nameof(UserBasketServiceTests));

            Assert.IsNotNull(initialBasket);
            Assert.IsTrue(initialProductQuantity == 1);
            Assert.That(initialBasket.Length == 1);

            Assert.IsNotNull(decreasedBasket);
            Assert.IsEmpty(decreasedBasket);
        }
    }
}
