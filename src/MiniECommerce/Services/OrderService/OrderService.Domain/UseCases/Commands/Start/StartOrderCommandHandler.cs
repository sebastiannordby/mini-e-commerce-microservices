using MediatR;
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
        private readonly IOrderService _orderService;

        public StartOrderCommandHandler(
            IInitializeOrderService initializeOrderService, 
            IOrderService orderService)
        {
            _initializeOrderService = initializeOrderService;
            _orderService = orderService;
        }

        public async Task<Guid> Handle(StartOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _initializeOrderService.Initialize(
                request.BuyersFullName,
                request.BuyersEmailAddress);

            return await _orderService.Save(order);
        }
    }
}
