using MediatR;
using OrderService.Domain.Repositories;
using OrderService.Library.Models;

namespace OrderService.Domain.UseCases.Queries.FindView
{
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
