using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Library.Models
{
    public sealed class ProductView
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal PricePerQuantity { get; set; }

        public ProductView()
        {

        }

        public ProductView(
            Guid id,
            int number,
            string name,
            string description,
            string category,
            decimal pricePerQuantity)
        {
            Id = id;
            Number = number;
            Name = name;
            Description = description;
            Category = category;
            PricePerQuantity = pricePerQuantity;
        }
    }
}
