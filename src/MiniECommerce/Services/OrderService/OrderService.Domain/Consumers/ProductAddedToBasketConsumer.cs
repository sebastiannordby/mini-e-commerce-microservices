using MassTransit;
using Microsoft.Extensions.Logging;
using MiniECommerce.Library.Events.BasketService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Consumers
{
    public sealed class ProductAddedToBasketConsumer : IConsumer<ProductAddedToBasketEvent>
    {
        private readonly ILogger<ProductAddedToBasketConsumer> _logger;

        public ProductAddedToBasketConsumer(ILogger<ProductAddedToBasketConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<ProductAddedToBasketEvent> context)
        {
            _logger.LogInformation("{0}", nameof(ProductAddedToBasketConsumer));

            return Task.CompletedTask;
        }
    }
}
