using MediatR;
using OrderService.Domain.Repositories;
using OrderService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.UseCases.Administration.Queries.ListOrders
{
    public sealed class AdmListOrderViewsQueryHandler : 
        IRequestHandler<AdmListOrderViewsQuery, IEnumerable<OrderView>?>
    {
        private readonly IAdminOrderViewRepository _adminViewRepository;

        public AdmListOrderViewsQueryHandler(
            IAdminOrderViewRepository adminViewRepository)
        {
            _adminViewRepository = adminViewRepository;
        }

        public async Task<IEnumerable<OrderView>?> Handle(AdmListOrderViewsQuery request, CancellationToken cancellationToken)
        {
            return await _adminViewRepository.List();
        }
    }
}
