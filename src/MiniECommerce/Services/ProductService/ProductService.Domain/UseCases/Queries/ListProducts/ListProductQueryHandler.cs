using MediatR;
using ProductService.DataAccess.Repositories;
using ProductService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.UseCases.Queries.ListProducts
{
    public sealed class ListProductQueryHandler : 
        IRequestHandler<ListProductQuery, IEnumerable<ProductView>>
    {
        private readonly IProductViewRepository _productViewRepository;

        public ListProductQueryHandler(
            IProductViewRepository productViewRepository)
        {
            _productViewRepository = productViewRepository;
        }

        public async Task<IEnumerable<ProductView>> Handle(
            ListProductQuery request, CancellationToken cancellationToken)
        {
            return await _productViewRepository.List();
        }
    }
}
