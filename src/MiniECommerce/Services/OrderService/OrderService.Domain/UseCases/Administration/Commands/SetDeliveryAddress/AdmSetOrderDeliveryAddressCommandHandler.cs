using MediatR;
using OrderService.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.UseCases.Administration.Commands.SetDeliveryAddress
{
    public sealed class AdmSetOrderDeliveryAddressCommandHandler : IRequestHandler<AdmSetOrderDeliveryAddressCommand, bool>
    {
        private readonly IOrderService _orderService;

        public AdmSetOrderDeliveryAddressCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<bool> Handle(AdmSetOrderDeliveryAddressCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderService.FindAsync(request.OrderId);
            if (order == null)
                return false;

            order.SetDeliveryAddress(
                request.AddressLine,
                request.PostalCode,
                request.PostalOffice,
                request.Country);

            await _orderService.SaveAsync(order);

            return true;
        }
    }
}
