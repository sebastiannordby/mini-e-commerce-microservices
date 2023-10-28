﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Library.Models
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal PricePerQuantity { get; set; }
        public string ImageUri { get; set; }

        public ProductDto()
        {

        }

        public ProductDto(
            Guid id,
            int number,
            string name,
            string description,
            string category,
            decimal pricePerQuantity,
            string imageUri)
        {
            Id = id;
            Number = number;
            Name = name;
            Description = description;
            Category = category;
            PricePerQuantity = pricePerQuantity;
            ImageUri = imageUri;
        }

    }
}
