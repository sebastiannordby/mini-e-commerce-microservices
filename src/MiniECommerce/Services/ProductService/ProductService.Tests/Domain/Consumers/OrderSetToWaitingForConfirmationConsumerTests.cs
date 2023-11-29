using MassTransit.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Authentication.Services;
using MiniECommerce.Library.Events.OrderService;
using NSubstitute;
using OrderService.Library.Enumerations;
using OrderService.Library.Models;
using ProductService.DataAccess;
using ProductService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Tests.Domain.Consumers
{
    public class OrderSetToWaitingForConfirmationConsumerTests : BaseProductServiceTest
    {
        [Test]
        public async Task TestConsumingEvent()
        {
            var harness = Services.GetRequiredService<ITestHarness>();
            var mediator = Services.GetRequiredService<MediatR.IMediator>();

            await harness.Start();
            await harness.Bus.Publish(new OrderSetToWaitingForConfirmationEvent(
                Substitute.For<OrderView>()));
            await harness.Stop();

            Assert.IsTrue(await harness.Published.Any<OrderSetToWaitingForConfirmationEvent>());
            Assert.IsTrue(await harness.Consumed.Any<OrderSetToWaitingForConfirmationEvent>());
        }

        [Test]
        public async Task TestConsumerInsertPurchaseStats()
        {
            var harness = Services.GetRequiredService<ITestHarness>();
            var mediator = Services.GetRequiredService<MediatR.IMediator>();
            var dbContext = Services.GetRequiredService<ProductDbContext>();
            var productRepository = Services.GetRequiredService<IProductRepository>();
            var currentUserService = Services.GetRequiredService<ICurrentUserService>();

            var insertedProductId = await productRepository.Create(new()
            {
                Category = nameof(TestConsumerInsertPurchaseStats),
                Description = nameof(TestConsumerInsertPurchaseStats),
                Name = nameof(TestConsumerInsertPurchaseStats),
                ImageUri = "https://hello.com/img.png",
                PricePerQuantity = 22
            });

            var productQuantityBought = 21;

            var order = new OrderView(
                id: Guid.NewGuid(),
                number: 1,
                status: OrderStatus.Confirmed,
                buyersFullName: currentUserService.UserFullName,
                buyersEmailAddress: currentUserService.UserEmail,
                deliveryAddress: null,
                deliveryAddressPostalCode: null,
                deliveryAddressPostalOffice: null,
                deliveryAddressCountry: null,
                invoiceAddress: null,
                invoiceAddressPostalCode: null,
                invoiceAddressPostalOffice: null,
                invoiceAddressCountry: null,
                lines: new List<OrderView.OrderLine>()
                {
                    new OrderView.OrderLine(
                        number: 1,
                        productId: insertedProductId,
                        productDescription: "Consumer Product",
                        quantity: productQuantityBought,
                        pricePerQuantity: productQuantityBought
                    )
                }
            );

            await harness.Start();
            await harness.Bus.Publish(new OrderSetToWaitingForConfirmationEvent(order));
            await harness.Stop();

            var productStats = await dbContext.ProductPurchaseStats
                .Where(x => x.BuyersEmailAddress == currentUserService.UserEmail)
                .Where(x => x.ProductId == insertedProductId)
                .FirstOrDefaultAsync();


            Assert.IsTrue(await harness.Published.Any<OrderSetToWaitingForConfirmationEvent>());
            Assert.IsTrue(await harness.Consumed.Any<OrderSetToWaitingForConfirmationEvent>());
            Assert.IsNotNull(productStats);
            Assert.AreEqual(productStats.TotalQuantityBought, productQuantityBought);
        }
    }
}
