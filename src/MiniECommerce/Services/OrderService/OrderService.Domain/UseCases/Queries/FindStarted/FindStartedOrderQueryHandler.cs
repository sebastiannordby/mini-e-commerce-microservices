using MediatR;
using MiniECommerce.Authentication.Services;
using OrderService.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.UseCases.Queries.FindStarted
{
    public sealed class FindStartedOrderQueryHandler : IRequestHandler<FindStartedOrderQuery, Guid?>
    {
        private readonly IOrderService _orderService;
        private readonly ICurrentUserService _currentUserService;

        public FindStartedOrderQueryHandler(
            IOrderService orderService, 
            ICurrentUserService currentUserService)
        {
            _orderService = orderService;
            _currentUserService = currentUserService;
        }

        public async Task<Guid?> Handle(FindStartedOrderQuery request, CancellationToken cancellationToken)
        {
            return await _orderService.GetStartedOrderId(_currentUserService.UserEmail);
        }
    }
}
