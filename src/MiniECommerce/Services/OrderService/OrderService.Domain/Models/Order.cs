using System;
using System.Linq;
using System.Text;
using FluentValidation;
using System.Threading.Tasks;
using FluentValidation.Results;
using System.Collections.Generic;
using BasketService.Library;
using OrderService.Library.Enumerations;

namespace OrderService.Domain.Models 
{
    public class Order
    {
        public Guid Id { get; private set; }
        public int Number { get; private set; }
        public OrderStatus Status { get; private set; }
        public string BuyersName { get; private set; }
        public string BuyersEmailAddress { get; private set; }

        public string? DeliveryAddressLine { get; private set; }
        public string? DeliveryAddressPostalCode { get; private set; }
        public string? DeliveryAddressPostalOffice { get; private set; }
        public string? DeliveryAddressCountry { get; private set; }

        public string? InvoiceAddressLine { get; private set; }
        public string? InvoiceAddressPostalCode { get; private set; }
        public string? InvoiceAddressPostalOffice { get; private set; }
        public string? InvoiceAddressCountry { get; private set; }

        private List<OrderLine> _orderLines = new List<OrderLine>();

        private IReadOnlyCollection<OrderLine> OrderLines => 
            _orderLines.AsReadOnly();

        internal Order(
            int newNumber,
            string buyersName,
            string buyersEmailAddress,
            string? deliveryAddress,
            string? deliveryAddressPostalCode,
            string? deliveryAddressPostalOffice,
            string? deliveryAddressCountry,
            string? invoiceAddress,
            string? invoiceAddressPostalCode,
            string? invoiceAddressPostalOffice,
            string? invoiceAddressCountry)
        {
            Number = newNumber;
            BuyersName = buyersName;
            BuyersEmailAddress = buyersEmailAddress;
            Status = OrderStatus.WaitingForDeliveryAddress;
            DeliveryAddressLine = deliveryAddress;
            DeliveryAddressPostalCode = deliveryAddressPostalCode;
            DeliveryAddressPostalOffice = deliveryAddressPostalOffice;
            DeliveryAddressCountry = deliveryAddressCountry;
            InvoiceAddressLine = invoiceAddress;
            InvoiceAddressPostalCode = invoiceAddressPostalCode;
            InvoiceAddressPostalOffice = invoiceAddressPostalOffice;
            InvoiceAddressCountry = invoiceAddressCountry;
        }

        internal Order(
            Guid id,
            int number,
            OrderStatus status,
            string buyersName,
            string buyersEmailAddress,
            string? deliveryAddress,
            string? deliveryAddressPostalCode,
            string? deliveryAddressPostalOffice,
            string? deliveryAddressCountry,
            string? invoiceAddress,
            string? invoiceAddressPostalCode,
            string? invoiceAddressPostalOffice,
            string? invoiceAddressCountry,
            IEnumerable<OrderLine> orderLines)
        {
            Id = id;
            Number = number;
            Status = status;
            BuyersName = buyersName;
            BuyersEmailAddress = buyersEmailAddress;
            DeliveryAddressLine = deliveryAddress;
            DeliveryAddressPostalCode = deliveryAddressPostalCode;
            DeliveryAddressPostalOffice = deliveryAddressPostalOffice;
            DeliveryAddressCountry = deliveryAddressCountry;
            InvoiceAddressLine = invoiceAddress;
            InvoiceAddressPostalCode = invoiceAddressPostalCode;
            InvoiceAddressPostalOffice = invoiceAddressPostalOffice;
            InvoiceAddressCountry = invoiceAddressCountry;
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

        internal void SetDeliveryAddress(
            string addressLine, 
            string postalCode, 
            string postalOffice, 
            string country)
        {
            DeliveryAddressLine = addressLine;
            DeliveryAddressPostalOffice = postalOffice;
            DeliveryAddressPostalCode = postalCode;
            DeliveryAddressCountry = country;
        }

        internal void SetInvoiceAddress(
            string addressLine,
            string postalCode,
            string postalOffice,
            string country)
        {
            InvoiceAddressLine = addressLine;
            InvoiceAddressPostalOffice = postalOffice;
            InvoiceAddressPostalCode = postalCode;
            InvoiceAddressCountry = country;
        }

        internal void Confirm()
        {
            if (Status != OrderStatus.WaitingForConfirmation)
                throw new Exception("Cannot confirm an order thats not waiting for confirmation.");

            Status = OrderStatus.Confirmed;
        }

        internal void SetWaitingForConfirmation()
        {
            if (Status >= OrderStatus.WaitingForConfirmation)
                throw new Exception("Cannot set to waiting for confirmation, when status is not InFill.");

            Status = OrderStatus.WaitingForConfirmation;
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