using MassTransit;
using Microsoft.Extensions.Logging;
using MiniECommerce.Library.Events.OrderService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.Consumers
{
    internal sealed class OrderSetToWaitingForConfirmationConsumer : IConsumer<OrderSetToWaitingForConfirmationEvent>
    {
        private readonly ILogger<OrderSetToWaitingForConfirmationConsumer> _logger;

        public OrderSetToWaitingForConfirmationConsumer(
            ILogger<OrderSetToWaitingForConfirmationConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<OrderSetToWaitingForConfirmationEvent> context)
        {
            _logger.LogInformation("Consuming: {0}, Consumed by: {1}", 
                nameof(OrderSetToWaitingForConfirmationEvent),
                nameof(OrderSetToWaitingForConfirmationEvent));

            return Task.CompletedTask;
        }
    }
}
