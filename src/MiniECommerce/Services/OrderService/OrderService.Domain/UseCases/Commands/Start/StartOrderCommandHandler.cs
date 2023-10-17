using MediatR;
using MiniECommerce.Gateway.Consumption.BasketService;
using OrderService.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.UseCases.Commands.Start
{
    public sealed class StartOrderCommandHandler : IRequestHandler<StartOrderCommand, Guid>
    {
        private readonly IInitializeOrderService _initializeOrderService;
        private readonly IGatewayBasketRepository _basketRepository;
        private readonly IOrderService _orderService;

        public StartOrderCommandHandler(
            IInitializeOrderService initializeOrderService, 
            IGatewayBasketRepository basketRepository,
            IOrderService orderService)
        {
            _initializeOrderService = initializeOrderService;
            _basketRepository = basketRepository;
            _orderService = orderService;
        }

        public async Task<Guid> Handle(StartOrderCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.BuyersEmailAddress))
                throw new ArgumentException($"{nameof(request.BuyersEmailAddress)} cannot be null/whitespace.");

            var order = await _initializeOrderService.Initialize(
                request.BuyersFullName,
                request.BuyersEmailAddress);

            var basketItems = await _basketRepository.GetList(request.RequestId, request.BuyersEmailAddress);
            if (basketItems == null)
                throw new Exception("Could not retrieve basket items.");

            if (!basketItems.Any())
                throw new Exception("There is no items in the basket.");

            var orderLines = basketItems
                .Select(basketItem => order.Create(basketItem));

            return await _orderService.Save(order);
        }
    }
}
