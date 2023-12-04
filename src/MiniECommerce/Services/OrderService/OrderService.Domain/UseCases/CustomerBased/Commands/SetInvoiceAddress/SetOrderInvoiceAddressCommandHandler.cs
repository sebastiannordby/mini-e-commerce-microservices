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

namespace OrderService.Domain.UseCases.CustomerBased.Commands.SetInvoiceAddress
{
    public sealed class SetOrderInvoiceAddressCommandHandler : IRequestHandler<SetOrderInvoiceAddressCommand, Result>
    {
        private readonly IOrderService _orderService;
        private readonly IOrderViewRepository _orderViewRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBus _bus;
        private readonly ILogger<SetOrderInvoiceAddressCommandHandler> _logger;

        public SetOrderInvoiceAddressCommandHandler(
            IOrderService orderService,
            IOrderViewRepository orderViewRepository,
            ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork,
            IBus bus,
            ILogger<SetOrderInvoiceAddressCommandHandler> logger)
        {
            _orderService = orderService;
            _orderViewRepository = orderViewRepository;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _bus = bus;
            _logger = logger;
        }

        public async Task<Result> Handle(SetOrderInvoiceAddressCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();

            var targetedOrderId = await _orderService.GetStartedOrderIdAsync(
                _currentUserService.UserEmail);
            if (!targetedOrderId.HasValue)
            {
                await _unitOfWork.RollbackAsync();
                return Result.Fail("Could not find a started order.");
            }

            var setAddressRes = await _orderService.SetInvoiceAddressAsync(
                _currentUserService.UserEmail,
                request.AddressLine,
                request.PostalCode,
                request.PostalOffice,
                request.Country);

            if (!setAddressRes)
            {
                await _unitOfWork.RollbackAsync();
                return Result.Fail("Could not set the address for your order.");
            }

            var setWaitingForConfirmationRes = await _orderService
                .SetToWaitingForPaymentAsync(_currentUserService.UserEmail);
            if (!setWaitingForConfirmationRes)
            {
                await _unitOfWork.RollbackAsync();
                return Result.Fail("Could not update status to waiting for confirmation.");
            }

            await _unitOfWork.CommitAsync();

            var targetedOrder = await _orderViewRepository.Find(
                targetedOrderId.Value);
            if (targetedOrder is null)
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
