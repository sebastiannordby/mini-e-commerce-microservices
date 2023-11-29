using OrderService.Domain.Models;
using OrderService.Library.Enumerations;
using System.Net.Sockets;

namespace OrderService.DataAccess.Models
{
    internal class OrderDao
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public OrderStatus Status { get; set; }
        public string BuyersFullName { get; set; }
        public string BuyersEmailAddress { get; set; }
        public string? DeliveryAddressLine { get; set; }
        public string? DeliveryAddressPostalCode { get; set; }
        public string? DeliveryAddressPostalOffice { get; set; }
        public string? DeliveryAddressCountry { get; set; }

        public string? InvoiceAddressLine { get; set; }
        public string? InvoiceAddressPostalCode { get; set; }
        public string? InvoiceAddressPostalOffice { get; set; }
        public string? InvoiceAddressCountry { get; set; }

        // Constuctor for EF
        public OrderDao()
        {

        }

        internal OrderDao(Order order)
        {
            Id = order.Id;
            Number = order.Number;
            Status = order.Status;
            BuyersFullName = order.BuyersName;
            BuyersEmailAddress = order.BuyersEmailAddress;

            DeliveryAddressLine = order.DeliveryAddressLine;
            DeliveryAddressPostalCode = order.DeliveryAddressPostalCode;
            DeliveryAddressPostalOffice = order.DeliveryAddressPostalOffice;
            DeliveryAddressCountry = order.DeliveryAddressCountry;

            InvoiceAddressLine = order.InvoiceAddressLine;
            InvoiceAddressPostalCode = order.InvoiceAddressPostalCode;
            InvoiceAddressPostalOffice = order.InvoiceAddressPostalOffice;
            InvoiceAddressCountry = order.InvoiceAddressCountry;
        }

        internal void Update(Order order)
        {
            BuyersFullName = order.BuyersName;
            Status = order.Status;

            DeliveryAddressLine = order.DeliveryAddressLine;
            DeliveryAddressPostalCode = order.DeliveryAddressPostalCode;
            DeliveryAddressPostalOffice = order.DeliveryAddressPostalOffice;
            DeliveryAddressCountry = order.DeliveryAddressCountry;

            InvoiceAddressLine = order.InvoiceAddressLine;
            InvoiceAddressPostalCode = order.InvoiceAddressPostalCode;
            InvoiceAddressPostalOffice = order.InvoiceAddressPostalOffice;
            InvoiceAddressCountry = order.InvoiceAddressCountry;
        }

        internal void SetDeliveryAddress(
            string addressLine, 
            string postalCode, 
            string postalOffice, 
            string country)
        {
            DeliveryAddressLine = addressLine;
            DeliveryAddressPostalCode = postalCode;
            DeliveryAddressPostalOffice = postalOffice;
            DeliveryAddressCountry = country;
        }

        internal void SetInvoiceAddress(
            string addressLine,
            string postalCode,
            string postalOffice,
            string country)
        {
            InvoiceAddressLine = addressLine;
            InvoiceAddressPostalCode = postalCode;
            InvoiceAddressPostalOffice = postalOffice;
            InvoiceAddressCountry = country;
        }

        internal void SetStatus(OrderStatus status)
        {
            Status = status;
        }
    }
}