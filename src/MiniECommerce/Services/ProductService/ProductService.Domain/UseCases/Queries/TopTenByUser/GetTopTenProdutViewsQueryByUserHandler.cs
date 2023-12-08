using MediatR;
using MiniECommerce.Authentication.Services;
using ProductService.Domain.Repositories;
using ProductService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.UseCases.Queries.TopTenByUser
{
    public sealed class GetTopTenProdutViewsQueryByUserHandler :
        IRequestHandler<GetTopTenProdutViewsQueryByUser, IEnumerable<ProductView>>
    {
        private readonly IProductPurchaseStatsRepository _purchaseStatsRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetTopTenProdutViewsQueryByUserHandler(
            IProductPurchaseStatsRepository purchaseStatsRepository, 
            ICurrentUserService currentUserService)
        {
            _purchaseStatsRepository = purchaseStatsRepository;
            _currentUserService = currentUserService;
        }

        public async Task<IEnumerable<ProductView>> Handle(GetTopTenProdutViewsQueryByUser request, CancellationToken cancellationToken)
        {
            var personalStats = await _purchaseStatsRepository.GetTopTenProductsByUser(
                _currentUserService.UserEmail);
            var peronalStatsCount = personalStats.Count();

            if (peronalStatsCount < 10)
            {
                var numMissingProducts = peronalStatsCount - 10;
                var topTenProducts = await _purchaseStatsRepository.GetTopTenProducts();
                var otherRecommendations = topTenProducts.Take(numMissingProducts);

                personalStats.AddRange(otherRecommendations);
            }

            return personalStats;
        }
    }
}
