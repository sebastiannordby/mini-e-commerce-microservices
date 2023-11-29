using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.UseCases.Administration.Commands.SetInvoiceAddress
{
    public sealed record AdmSetOrderInvoiceAddressCommand(
        Guid OrderId,
        string AddressLine,
        string PostalCode,
        string PostalOffice,
        string Country
    ) : IRequest<bool>;
}
