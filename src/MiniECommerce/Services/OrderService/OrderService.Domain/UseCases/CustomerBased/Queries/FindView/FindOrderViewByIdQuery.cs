using MediatR;
using OrderService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.UseCases.CustomerBased.Queries.FindView
{
    public sealed class FindOrderViewByIdQuery : IRequest<OrderView?>
    {
        public Guid OrderId { get; private set; }

        public FindOrderViewByIdQuery(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
