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
        public IEnumerable<OrderLine> Lines { get; set; }

        public OrderView(
            Guid id, 
            int number,
            IEnumerable<OrderLine> lines)
        {
            Id = id;
            Number = number;
            Lines = lines;
        }

        public class OrderLine
        {
            public int Number { get; set; }
            public Guid ProductId { get; set; }
            public string ProductDescription { get; set; }
            public int Quantity { get; set; }
            public decimal PricePerQuantity { get; set; }

            public OrderLine()
            {

            }

            public OrderLine(
                int number,
                Guid productId,
                string productDescription,
                int quantity,
                decimal pricePerQuantity)
            {
                Number = number;
                ProductId = productId;
                ProductDescription = productDescription;
                Quantity = quantity;
                PricePerQuantity = pricePerQuantity;
            }
        }
    }
}
