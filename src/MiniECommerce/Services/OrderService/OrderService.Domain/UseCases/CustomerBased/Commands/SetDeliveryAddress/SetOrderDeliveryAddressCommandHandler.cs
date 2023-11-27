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

namespace OrderService.Domain.UseCases.CustomerBased.Commands.SetDeliveryAddress
{
    public class SetOrderDeliveryAddressCommandHandler : IRequestHandler<SetOrderDeliveryAddressCommand, Result>
    {
        private readonly IOrderService _orderService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SetOrderDeliveryAddressCommandHandler> _logger;

        public SetOrderDeliveryAddressCommandHandler(
            IOrderService orderService,
            ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork,
            ILogger<SetOrderDeliveryAddressCommandHandler> logger)
        {
            _orderService = orderService;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result> Handle(SetOrderDeliveryAddressCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();

            var targetedOrderId = await _orderService.GetStartedOrderIdAsync(
                _currentUserService.UserEmail);
            if (!targetedOrderId.HasValue)
            {
                await _unitOfWork.RollbackAsync();
                return Result.Fail("Could not find a started order.");
            }

            var setAddressRes = await _orderService.SetDeliveryAddressAsync(
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

            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
