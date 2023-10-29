using MediatR;
using OrderService.Library.Enumerations;
using OrderService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.UseCases.Administration.Queries.ListOrders
{
    public sealed record ListOrderViewsQuery(
        IEnumerable<OrderStatus>? Statuses = null
    ) : IRequest<IEnumerable<OrderView>>;
}
