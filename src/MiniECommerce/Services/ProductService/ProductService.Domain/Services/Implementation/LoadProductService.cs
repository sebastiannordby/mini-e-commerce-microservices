using FluentValidation;
using ProductService.Domain.Models;
using ProductService.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.Services
{
    internal sealed class LoadProductService : ILoadProductService
    {
        private readonly IProductValidationService _validationService;

        public LoadProductService(
            IProductValidationService validationService)
        {
            _validationService = validationService;
        }

        public async Task<Product> LoadAsync(
            Guid id, 
            string number, 
            string name, 
            string description, 
            string category)
        {
            var product = new Product(
                id, 
                number,
                name,
                description,
                category);

            var validationFailures = await product
                .Validate(_validationService);
            if (validationFailures.Any())
                throw new ValidationException(validationFailures);

            return product;
        }
    }
}
