using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.UseCases.UserBased.Commands.Start
{
    public sealed record StartOrderCommand : IRequest<Guid>;
}
