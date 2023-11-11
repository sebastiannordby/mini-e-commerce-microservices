using MediatR;
using OrderService.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.UseCases.Administration.Commands.SetAddress
{
    public sealed class AdmSetOrderAddressCommandHandler : IRequestHandler<AdmSetOrderAddressCommand, bool>
    {
        private readonly IOrderService _orderService;

        public AdmSetOrderAddressCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<bool> Handle(AdmSetOrderAddressCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderService.FindAsync(request.OrderId);
            if (order == null)
                return false;

            order.SetAddress(
                request.AddressLine,
                request.PostalCode,
                request.PostalOffice,
                request.Country);

            await _orderService.SaveAsync(order);

            return true;
        }
    }
}
