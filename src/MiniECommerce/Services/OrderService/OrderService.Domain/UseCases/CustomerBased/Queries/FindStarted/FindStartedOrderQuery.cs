using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.UseCases.CustomerBased.Queries.FindStarted
{
    public sealed record FindStartedOrderQuery : IRequest<Guid?>;
}
