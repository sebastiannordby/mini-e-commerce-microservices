using MediatR;
using OrderService.Domain.Repositories;
using OrderService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.UseCases.Queries.FindView
{
    public sealed class FindOrderViewByIdQuery : IRequest<OrderView?>
    {
        public Guid OrderId { get; private set; }

        public FindOrderViewByIdQuery(Guid orderId)
        {
            OrderId = orderId;
        }
    }

    public sealed class FindOrderViewByIdQueryHandler : 
        IRequestHandler<FindOrderViewByIdQuery, OrderView?>
    {
        private readonly IOrderViewRepository _orderViewRepository;

        public FindOrderViewByIdQueryHandler(
            IOrderViewRepository orderViewRepository)
        {
            _orderViewRepository = orderViewRepository;
        }

        public async Task<OrderView?> Handle(FindOrderViewByIdQuery request, CancellationToken cancellationToken)
        {
            return await _orderViewRepository.Find(request.OrderId);
        }
    }
}
