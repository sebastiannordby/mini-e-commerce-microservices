﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.DataAccess.Models
{
    public sealed class ProductDao
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal PricePerQuantity { get; set; }

        public ProductDao()
        {

        }

        public ProductDao(
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

        internal void Update(
            string name, 
            string description, 
            string category, 
            decimal pricePerQuantity)
        {
            Name = name;
            Description = description;
            Category = category;
            PricePerQuantity = pricePerQuantity;
        }
    }
}
