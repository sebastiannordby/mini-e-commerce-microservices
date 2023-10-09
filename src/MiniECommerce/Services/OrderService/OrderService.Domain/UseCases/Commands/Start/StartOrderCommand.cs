using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.UseCases.Commands.Start
{
    public sealed record StartOrderCommand(
        Guid RequestId,
        Guid BasketId,
        string BuyersFullName,
        string BuyersEmailAddress
    ) : IRequest<Guid>;
}
