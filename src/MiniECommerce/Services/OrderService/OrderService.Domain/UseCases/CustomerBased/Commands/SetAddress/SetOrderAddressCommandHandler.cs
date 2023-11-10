using MassTransit;
using MediatR;
using MiniECommerce.Authentication.Services;
using MiniECommerce.Library.Events.OrderService;
using OrderService.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.UseCases.CustomerBased.Commands.SetAddress
{
    public class SetOrderAddressCommandHandler : IRequestHandler<SetOrderAddressCommand, bool>
    {
        private readonly IOrderService _orderService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBus _bus;

        public SetOrderAddressCommandHandler(
            IOrderService orderService,
            ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork,
            IBus bus)
        {
            _orderService = orderService;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _bus = bus;
        }

        public async Task<bool> Handle(SetOrderAddressCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();

            var setAddressRes = await _orderService.SetAddress(
                _currentUserService.UserEmail,
                request.AddressLine,
                request.PostalCode,
                request.PostalOffice,
                request.Country);

            var setWaitingForConfirmationRes = await _orderService.SetWaitingForConfirmation(
                _currentUserService.UserEmail);

            if(!setAddressRes || !setWaitingForConfirmationRes)
            {
                await _unitOfWork.RollbackAsync();
                return false;
            }

            await _unitOfWork.CommitAsync();

            await _bus.Publish(new OrderSetToWaitingForConfirmationEvent(
                _currentUserService.UserFullName
            ));

            return true;
        }
    }
}
