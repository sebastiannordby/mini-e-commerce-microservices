using System;
using System.Linq;
using System.Text;
using FluentValidation;
using System.Threading.Tasks;
using FluentValidation.Results;
using System.Collections.Generic;
using BasketService.Library;

namespace OrderService.Domain.Models 
{
    public class Order
    {
        public Guid Id { get; private set; }
        public int Number { get; private set; }
        public OrderStatus Status { get; private set; }
        public string BuyersName { get; private set; }
        public string BuyersEmailAddress { get; private set; }
        public string? AddressLine { get; private set; }
        public string? PostalCode { get; private set; }
        public string? PostalOffice { get; private set; }
        public string? Country { get; private set; }

        private List<OrderLine> _orderLines = new List<OrderLine>();

        private IReadOnlyCollection<OrderLine> OrderLines => 
            _orderLines.AsReadOnly();

        public enum OrderStatus
        {
            InFill = 0,
            WaitingForConfirmation = 1,
            Confirmed = 2,
            Delivered = 3
        }

        internal Order(
            int newNumber,
            string buyersName,
            string buyersEmailAddress)
        {
            Number = newNumber;
            BuyersName = buyersName;
            BuyersEmailAddress = buyersEmailAddress;
            Status = OrderStatus.InFill;
        }

        internal Order(
            Guid id,
            int number,
            string buyersName,
            string buyersEmailAddress,
            string? addressLine,
            string? postalCode,
            string? postalOffice,
            string? country,
            IEnumerable<OrderLine> orderLines)
        {
            Id = id;
            Number = number;
            BuyersName = buyersName;
            BuyersEmailAddress = buyersEmailAddress;
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

        internal OrderLine Create(BasketItemView basketItem)
        {
            if (basketItem == null)
                throw new ArgumentNullException(nameof(basketItem));

            var orderLineNumbers = _orderLines
                .Select(x => x.Number);

            var orderLine = new OrderLine()
            {
                Number = orderLineNumbers.Any() ? orderLineNumbers.Max() + 1 : 1,
                PricePerQuantity = basketItem.PricePerQuantity,
                ProductDescription = basketItem.ProductName,
                ProductId = basketItem.ProductId,
                Quantity = basketItem.Quantity
            };

            _orderLines.Add(orderLine);

            return orderLine;
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