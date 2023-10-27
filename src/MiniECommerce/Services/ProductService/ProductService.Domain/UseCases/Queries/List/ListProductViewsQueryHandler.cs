using MediatR;
using ProductService.DataAccess.Repositories;
using ProductService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.UseCases.Queries.List
{
    public sealed class ListProductViewsQueryHandler : 
        IRequestHandler<ListProductViewsQuery, IEnumerable<ProductView>>
    {
        private readonly IProductViewRepository _productViewRepository;

        public ListProductViewsQueryHandler(
            IProductViewRepository productViewRepository)
        {
            _productViewRepository = productViewRepository;
        }

        public async Task<IEnumerable<ProductView>> Handle(
            ListProductViewsQuery request, CancellationToken cancellationToken)
        {
            return await _productViewRepository.List(
                request.FromPricePerQuantity,
                request.ToPricePerQuantity,
                request.Categories);
        }
    }
}
