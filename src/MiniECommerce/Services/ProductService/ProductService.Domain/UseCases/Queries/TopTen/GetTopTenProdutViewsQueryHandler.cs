using MediatR;
using MiniECommerce.Authentication.Services;
using ProductService.Domain.Repositories;
using ProductService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.UseCases.Queries.TopTen
{
    public sealed class GetTopTenProdutViewsQueryHandler :
        IRequestHandler<GetTopTenProdutViewsQuery, IEnumerable<ProductView>>
    {
        private readonly IProductPurchaseStatsRepository _purchaseStatsRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetTopTenProdutViewsQueryHandler(
            IProductPurchaseStatsRepository purchaseStatsRepository, 
            ICurrentUserService currentUserService)
        {
            _purchaseStatsRepository = purchaseStatsRepository;
            _currentUserService = currentUserService;
        }

        public async Task<IEnumerable<ProductView>> Handle(GetTopTenProdutViewsQuery request, CancellationToken cancellationToken)
        {
            var personalStats = await _purchaseStatsRepository.GetTopTenProductsByUser(
                _currentUserService.UserEmail);

            return personalStats;
        }
    }
}
