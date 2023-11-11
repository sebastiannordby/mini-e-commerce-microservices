using FluentResults;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using MiniECommerce.Authentication.Services;
using MiniECommerce.Library.Events.OrderService;
using OrderService.Domain.Repositories;
using OrderService.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.UseCases.CustomerBased.Commands.SetAddress
{
    public class SetOrderAddressCommandHandler : IRequestHandler<SetOrderAddressCommand, Result>
    {
        private readonly IOrderService _orderService;
        private readonly IOrderViewRepository _orderViewRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBus _bus;
        private readonly ILogger<SetOrderAddressCommandHandler> _logger;

        public SetOrderAddressCommandHandler(
            IOrderService orderService,
            IOrderViewRepository orderViewRepository,
            ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork,
            IBus bus,
            ILogger<SetOrderAddressCommandHandler> logger)
        {
            _orderService = orderService;
            _orderViewRepository = orderViewRepository;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _bus = bus;
            _logger = logger;
        }

        public async Task<Result> Handle(SetOrderAddressCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();

            var targetedOrderId = await _orderService.GetStartedOrderIdAsync(
                _currentUserService.UserEmail);
            if (!targetedOrderId.HasValue)
            {
                await _unitOfWork.RollbackAsync();
                return Result.Fail("Could not find a started order.");
            }

            var setAddressRes = await _orderService.SetAddressAsync(
                _currentUserService.UserEmail,
                request.AddressLine,
                request.PostalCode,
                request.PostalOffice,
                request.Country);

            var setWaitingForConfirmationRes = await _orderService.SetWaitingForConfirmationAsync(
                _currentUserService.UserEmail);

            if(!setAddressRes)
            {
                await _unitOfWork.RollbackAsync();
                return Result.Fail("Could not set the address for your order.");
            }

            if(!setWaitingForConfirmationRes)
            {
                await _unitOfWork.RollbackAsync();
                return Result.Fail("Could not update status to waiting for confirmation.");
            }

            await _unitOfWork.CommitAsync();

            var targetedOrder = await _orderViewRepository.Find(
                targetedOrderId.Value);
            if(targetedOrder is null)
                return Result
                    .Ok()
                    .WithError($"Could not publish {nameof(OrderSetToWaitingForConfirmationEvent)}");

            await _bus.Publish(new OrderSetToWaitingForConfirmationEvent(
                targetedOrder
            ));

            return Result.Ok();
        }
    }
}
