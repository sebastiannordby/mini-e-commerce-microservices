using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.UseCases.Commands.SetAddress
{
    public sealed record SetOrderAddressCommand(
        string AddressLine,
        string PostalCode,
        string PostalOffice,
        string Country
    ) : IRequest<bool>;
}
