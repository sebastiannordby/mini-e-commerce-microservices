using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Models 
{
    public class Order
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public string BuyersName { get; set; }
        public string AddressLine { get; set; }
        public string PostalCode { get; set; }
        public string PostalOffice { get; set; }
        public string Country { get; set; }

        private List<OrderLine> _orderLines = new List<OrderLine>();

        internal Order()
        {

        }

        internal Order(
            Guid id,
            int number,
            string buyersName,
            string addressLine,
            string postalCode,
            string postalOffice,
            string country,
            IEnumerable<OrderLine> orderLines)
        {
            Id = id;
            Number = number;
            BuyersName = buyersName;
            AddressLine = addressLine;
            PostalCode = postalCode;
            PostalOffice = postalOffice;
            Country = country;
            _orderLines = orderLines?.ToList() ?? new();
        }

        public IEnumerable<ValidationFailure> Validate()
        {
            if (Number <= 0)
                yield return new(
                    nameof(Number), "Number must be higher than 0.");

            if (string.IsNullOrWhiteSpace(BuyersName))
                yield return new(
                    nameof(BuyersName), "Cannot be null/whitespace.");

            if (_orderLines.Any())
            {
                var linesHasUniqueNumbers = _orderLines
                    .GroupBy(x => x.Number)
                    .Where(x => x.Count() > 1)
                    .Any() == false;

                if (!linesHasUniqueNumbers)
                    yield return new(
                        "OrderLines", "Same orderline number is used multiple times.");
            }
        }

        public class OrderLine
        {
            public Guid Id { get; set; }
            public int Number { get; set; }
            public Guid ProductId { get; set; }
            public string ProductDescription { get; set; }
            public int Quantity { get; set; }
            public decimal PricePerQuantity { get; set; }

            public OrderLine()
            {

            }

            private OrderLine(
                Guid id,
                int number,
                Guid productId,
                string productDescription,
                int quantity,
                decimal pricePerQuantity)
            {
                Id = id;
                Number = number;
                ProductId = productId;
                ProductDescription = productDescription;
                Quantity = quantity;
                PricePerQuantity = pricePerQuantity;
            }

            public static OrderLine Load(
                Guid id,
                int number,
                Guid productId,
                string productDescription,
                int quantity,
                decimal pricePerQuantity)
            {
                return new OrderLine(
                    id,
                    number,
                    productId,
                    productDescription,
                    quantity,
                    pricePerQuantity
                );
            }
        }
    }
}