using MediatR;
using OrderService.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.UseCases.Administration.Commands.SetWaitForConfirmation
{
    public sealed class AdmSetOrderWaitForConfirmationCommandHandler : IRequestHandler<AdmSetOrderWaitForConfirmationCommand, bool>
    {
        private readonly IOrderService _orderService;

        public AdmSetOrderWaitForConfirmationCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<bool> Handle(AdmSetOrderWaitForConfirmationCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderService.FindAsync(request.OrderId);
            if (order is null)
                return false;

            order.SetWaitingForConfirmation();
            await _orderService.Save(order);

            return true;
        }
    }
}
