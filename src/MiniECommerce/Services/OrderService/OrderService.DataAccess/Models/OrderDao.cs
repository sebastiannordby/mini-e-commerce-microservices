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
        public string? AddressLine { get; set; }
        public string? PostalCode { get; set; }
        public string? PostalOffice { get; set; }
        public string? Country { get; set; }

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
            AddressLine = order.AddressLine;
            PostalCode = order.PostalCode;
            PostalOffice = order.PostalOffice;
            Country = order.Country;
        }

        internal void Update(Order order)
        {
            BuyersFullName = order.BuyersName;
            Status = order.Status;
            AddressLine = order.AddressLine;
            PostalCode = order.PostalCode;
            PostalOffice = order.PostalOffice;
            Country = order.Country;
        }

        internal void SetAddress(
            string addressLine, 
            string postalCode, 
            string postalOffice, 
            string country)
        {
            AddressLine = addressLine;
            PostalCode = postalCode;
            PostalOffice = postalOffice;
            Country = country;
        }

        internal void SetStatus(OrderStatus status)
        {
            Status = status;
        }
    }
}