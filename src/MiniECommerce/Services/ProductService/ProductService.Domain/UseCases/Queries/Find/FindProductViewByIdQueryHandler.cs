using MediatR;
using ProductService.Domain.Repositories;
using ProductService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.UseCases.Queries.Find
{
    public sealed class FindProductViewByIdQueryHandler : IRequestHandler<FindProductViewByIdQuery, ProductView?>
    {
        private readonly IProductViewRepository _viewRepository;

        public FindProductViewByIdQueryHandler(
            IProductViewRepository viewRepository)
        {
            _viewRepository = viewRepository;
        }

        public async Task<ProductView?> Handle(FindProductViewByIdQuery request, CancellationToken cancellationToken)
        {
            return await _viewRepository.Find(request.ProductId);
        }
    }
}
