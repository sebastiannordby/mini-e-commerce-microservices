using MassTransit;
using Microsoft.Extensions.Logging;
using MiniECommerce.Library.Events.OrderService;
using MiniECommerce.Library.Events.PurchaseService;
using OrderService.Domain.Repositories;
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
        private readonly IOrderViewRepository _orderViewRepository;
        private readonly IBus _bus;

        public OrderPurchasedEventConsumer(
            IOrderService orderService,
            IBus bus,
            ILogger<OrderPurchasedEventConsumer> logger,
            IOrderViewRepository orderViewRepository)
        {
            _orderService = orderService;
            _bus = bus;
            _logger = logger;
            _orderViewRepository = orderViewRepository;
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

            var orderView = await _orderViewRepository.Find(order.Id);
            if(orderView is not null)
            {
                await _bus.Publish(new OrderSetToWaitingForConfirmationEvent(orderView));
            }
        }
    }
}
