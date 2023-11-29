using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.UseCases.CustomerBased.Commands.SetDeliveryAddress
{
    public sealed record SetOrderDeliveryAddressCommand(
        string AddressLine,
        string PostalCode,
        string PostalOffice,
        string Country
    ) : IRequest<Result>;
}
