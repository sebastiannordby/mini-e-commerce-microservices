using OrderService.Library.Enumerations;
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
        public OrderStatus Status { get; set; }
        public string BuyersFullName { get; private set; }
        public string BuyersEmailAddress { get; set; }
        public string? AddressLine { get; set; }
        public string? PostalCode { get; set; }
        public string? PostalOffice { get; set; }
        public string? Country { get; set; }
        public IEnumerable<OrderLine> Lines { get; set; }

        public OrderView()
        {

        }

        public OrderView(
            Guid id, 
            int number,
            OrderStatus status,
            string buyersFullName,
            string buyersEmailAddress,
            string? addressLine,
            string? postalCode,
            string? postalOffice,
            string? country,
            IEnumerable<OrderLine> lines)
        {
            Id = id;
            Number = number;
            Status = status;
            BuyersFullName = buyersFullName;
            BuyersEmailAddress = buyersEmailAddress;
            AddressLine = addressLine;
            PostalCode = postalCode;
            PostalOffice = postalOffice;
            Country = country;
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
