using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Library.Models
{
    public class OrderView
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public DateTime PlacedDateTime { get; set; }

        public class OrderLine
        {
            public int Number { get; set; }
            public Guid ProductId { get; set; }
            public string ProductDescription { get; set; }
            public int Quantity { get; set; }
            public decimal PricePerQuantity { get; set; }
        }
    }
}
