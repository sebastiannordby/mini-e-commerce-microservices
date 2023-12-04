using MassTransit;
using Microsoft.Extensions.Logging;
using MiniECommerce.Library.Events.PurchaseService;
using OrderService.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Consumers
{
    public sealed class OrderPurchasedEventConsumer : IConsumer<OrderPurchasedEvent>
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderPurchasedEventConsumer> _logger;

        public OrderPurchasedEventConsumer(
            IOrderService orderService,
            ILogger<OrderPurchasedEventConsumer> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<OrderPurchasedEvent> context)
        {
            var order = await _orderService.FindAsync(context.Message.OrderId);
            if(order == null)
            {
                _logger.LogInformation("{0}: Could not find order with id({1})",
                    nameof(OrderPurchasedEventConsumer), context.Message.OrderId);

                return;
            }
            _logger.LogInformation("{0}: Setting order status to waiting for confirmation({1})",
                nameof(OrderPurchasedEventConsumer), context.Message.OrderId);

            order.SetWaitingForConfirmation();
            await _orderService.SaveAsync(order);
        }
    }
}
