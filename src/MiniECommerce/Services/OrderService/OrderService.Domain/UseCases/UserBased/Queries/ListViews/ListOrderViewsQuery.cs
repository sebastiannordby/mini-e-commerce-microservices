using MediatR;
using OrderService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.UseCases.UserBased.Queries.ListViews
{
    public sealed record ListOrderViewsQuery : IRequest<IEnumerable<OrderView>>;
}
