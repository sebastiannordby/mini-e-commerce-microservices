using MediatR;
using MiniECommerce.Authentication.Services;
using OrderService.Domain.Repositories;
using OrderService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.UseCases.UserBased.Queries.ListViews
{
    public sealed class ListOrderViewsQueryHandler :
        IRequestHandler<ListOrderViewsQuery, IEnumerable<OrderView>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IOrderViewRepository _orderViewRepository;

        public ListOrderViewsQueryHandler(
            ICurrentUserService currentUserService, 
            IOrderViewRepository orderViewRepository)
        {
            _currentUserService = currentUserService;
            _orderViewRepository = orderViewRepository;
        }

        public async Task<IEnumerable<OrderView>> Handle(ListOrderViewsQuery request, CancellationToken cancellationToken)
        {
            return await _orderViewRepository.List(
                _currentUserService.UserEmail);
        }
    }
}
