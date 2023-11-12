using MassTransit;
using Microsoft.Extensions.Logging;
using MiniECommerce.Library.Events.OrderService;
using ProductService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.Consumers
{
    public sealed class OrderSetToWaitingForConfirmationConsumer : IConsumer<OrderSetToWaitingForConfirmationEvent>
    {
        private readonly IProductPurchaseStatsRepository _purchaseStatsRepository;
        private readonly ILogger<OrderSetToWaitingForConfirmationConsumer> _logger;

        public OrderSetToWaitingForConfirmationConsumer(
            IProductPurchaseStatsRepository productPurchaseStatsRepository,
            ILogger<OrderSetToWaitingForConfirmationConsumer> logger)
        {
            _purchaseStatsRepository = productPurchaseStatsRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<OrderSetToWaitingForConfirmationEvent> context)
        {
            _logger.LogInformation("Consuming: {0}, Consumed by: {1}", 
                nameof(OrderSetToWaitingForConfirmationEvent),
                nameof(OrderSetToWaitingForConfirmationConsumer));

            var order = context.Message.Order;

            foreach(var orderLine in order.Lines)
            {
                _logger.LogInformation("{0}: Inserting purchase stats for {1}, quantity: {2}",
                    nameof(OrderSetToWaitingForConfirmationConsumer),
                    orderLine.ProductDescription,
                    orderLine.Quantity);

                await _purchaseStatsRepository.InsertOrUpdateAsync(
                    order.BuyersEmailAddress,
                    orderLine.ProductId,
                    orderLine.Quantity
                );
            }
        }
    }
}
