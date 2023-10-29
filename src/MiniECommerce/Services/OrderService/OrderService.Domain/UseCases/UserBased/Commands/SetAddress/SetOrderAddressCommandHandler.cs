using MassTransit;
using MediatR;
using MiniECommerce.Authentication.Services;
using OrderService.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.UseCases.UserBased.Commands.SetAddress
{
    public class SetOrderAddressCommandHandler : IRequestHandler<SetOrderAddressCommand, bool>
    {
        private readonly IOrderService _orderService;
        private readonly ICurrentUserService _currentUserService;

        public SetOrderAddressCommandHandler(
            IOrderService orderService,
            ICurrentUserService currentUserService)
        {
            _orderService = orderService;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(SetOrderAddressCommand request, CancellationToken cancellationToken)
        {
            return await _orderService.SetAddress(
                _currentUserService.UserEmail,
                request.AddressLine,
                request.PostalCode,
                request.PostalOffice,
                request.Country
            );
        }
    }
}
