using MediatR;
using ProductService.DataAccess.Repositories;
using ProductService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.UseCases.Queries.Find
{
    public sealed class FindProductByIdQueryHandler : IRequestHandler<FindProductByIdQuery, ProductDto?>
    {
        private readonly IProductRepository _productRepository;

        public FindProductByIdQueryHandler(
            IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto?> Handle(FindProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.Find(request.ProductId);
        }
    }
}
