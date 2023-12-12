using MediatR;
using ProductService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.UseCases.Queries.ListCategories
{
    public sealed class ListProductCategoriesQueryHandler : 
        IRequestHandler<ListProductCategoriesQuery, IEnumerable<string>>
    {
        private IProductRepository _productRepository;

        public ListProductCategoriesQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<string>> Handle(ListProductCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.ListCategories();
        }
    }
}
