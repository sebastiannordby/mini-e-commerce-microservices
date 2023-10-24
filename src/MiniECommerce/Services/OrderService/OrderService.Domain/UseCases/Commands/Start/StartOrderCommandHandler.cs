using MediatR;
using MiniECommerce.Authentication.Services;
using MiniECommerce.Library.Services.BasketService;
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
        private readonly ICurrentUserService _currentUserService;

        public StartOrderCommandHandler(
            IInitializeOrderService initializeOrderService,
            IGatewayBasketRepository basketRepository,
            IOrderService orderService,
            ICurrentUserService currentUserService)
        {
            _initializeOrderService = initializeOrderService;
            _basketRepository = basketRepository;
            _orderService = orderService;
            _currentUserService = currentUserService;
        }

        public async Task<Guid> Handle(StartOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _initializeOrderService.Initialize(
                request.BuyersFullName,
                _currentUserService.UserEmail);

            var basketItems = await _basketRepository.GetList(
                request.RequestId, _currentUserService.UserEmail);
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
