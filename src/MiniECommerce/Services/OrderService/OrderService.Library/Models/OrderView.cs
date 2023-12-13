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
        public string? DeliveryAddressLine { get; set; }
        public string? DeliveryAddressPostalCode { get; set; }
        public string? DeliveryAddressPostalOffice { get; set; }
        public string? DeliveryAddressCountry { get; set; }

        public string? InvoiceAddressLine { get; set; }
        public string? InvoiceAddressPostalCode { get; set; }
        public string? InvoiceAddressPostalOffice { get; set; }
        public string? InvoiceAddressCountry { get; set; }

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
            string? deliveryAddress,
            string? deliveryAddressPostalCode,
            string? deliveryAddressPostalOffice,
            string? deliveryAddressCountry,
            string? invoiceAddress,
            string? invoiceAddressPostalCode,
            string? invoiceAddressPostalOffice,
            string? invoiceAddressCountry,
            IEnumerable<OrderLine> lines)
        {
            Id = id;
            Number = number;
            Status = status;
            BuyersFullName = buyersFullName;
            BuyersEmailAddress = buyersEmailAddress;
            DeliveryAddressLine = deliveryAddress;
            DeliveryAddressPostalCode = deliveryAddressPostalCode;
            DeliveryAddressPostalOffice = deliveryAddressPostalOffice;
            DeliveryAddressCountry = deliveryAddressCountry;
            InvoiceAddressLine = invoiceAddress;
            InvoiceAddressPostalCode = invoiceAddressPostalCode;
            InvoiceAddressPostalOffice = invoiceAddressPostalOffice;
            InvoiceAddressCountry = invoiceAddressCountry;
            Lines = lines;
        }

        public class OrderLine
        {
            public int Number { get; set; }
            public Guid ProductId { get; set; }
            public string ProductName { get; set; }
            public string ProductCategory { get; set; }
            public string ProductDescription { get; set; }
            public int Quantity { get; set; }
            public decimal PricePerQuantity { get; set; }

            public OrderLine()
            {

            }

            public OrderLine(
                int number,
                Guid productId,
                string productName,
                string productCategory,
                string productDescription,
                int quantity,
                decimal pricePerQuantity)
            {
                Number = number;
                ProductId = productId;
                ProductName = productName;
                ProductCategory = productCategory;
                ProductDescription = productDescription;
                Quantity = quantity;
                PricePerQuantity = pricePerQuantity;
            }
        }
    }
}
