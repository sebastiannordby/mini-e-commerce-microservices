using MediatR;
using OrderService.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.UseCases.Administration.Commands.Confirm
{
    public sealed class AdmConfirmOrderCommandHandler : IRequestHandler<AdmConfirmOrderCommand, bool>
    {
        private readonly IOrderService _orderService;

        public AdmConfirmOrderCommandHandler(
            IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<bool> Handle(AdmConfirmOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderService.FindAsync(request.OrderId);
            if (order is null)
                return false;

            order.Confirm();
            await _orderService.SaveAsync(order);

            return true;
        }
    }
}
