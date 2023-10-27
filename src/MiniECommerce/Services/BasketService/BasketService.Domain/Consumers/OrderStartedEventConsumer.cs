using BasketService.Domain.Services;
using MassTransit;
using Microsoft.Extensions.Logging;
using MiniECommerce.Library.Events.BasketService;
using MiniECommerce.Library.Events.OrderService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Domain.Consumers
{
    public sealed class OrderStartedEventConsumer : IConsumer<OrderStartedEvent>
    {
        private readonly IUserBasketService _userBasketService;
        private readonly ILogger<OrderStartedEventConsumer> _logger;

        public OrderStartedEventConsumer(
            IUserBasketService userBasketService,
            ILogger<OrderStartedEventConsumer> logger)
        {
            _userBasketService = userBasketService;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<OrderStartedEvent> context)
        {
            _logger.LogInformation("{0}: Clearing user basket.");
            await _userBasketService.ClearBasket(context.Message.UserEmail);
            _logger.LogInformation("{0}: User basket cleared.");

            await context.RespondAsync(new BasketClearedEvent(
                context.Message.UserEmail));
        }
    }
}
