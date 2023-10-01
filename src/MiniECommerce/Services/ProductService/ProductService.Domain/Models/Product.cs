using FluentValidation.Results;
using ProductService.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.Models
{
    public sealed class Product
    {
        public Guid Id { get; private set; }
        public string Number { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Category { get; private set; }

        internal Product(
            Guid id,
            string number, 
            string name, 
            string description, 
            string category)
        {
            Id = id;
            Number = number;
            Name = name;
            Description = description;
            Category = category;
        }

        public async Task<List<ValidationFailure>> Validate(
            IProductValidationService validationService)
        {
            var result = new List<ValidationFailure>();

            if(!await validationService.IsNumberUnique(Id, Number))
            {
                result.Add(new(
                    propertyName: nameof(Number), 
                    errorMessage: "Must be unique."));
            }

            return result;
        }
    }
}
