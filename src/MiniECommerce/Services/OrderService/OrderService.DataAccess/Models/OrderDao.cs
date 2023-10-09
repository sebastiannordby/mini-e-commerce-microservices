using OrderService.Domain.Models;
using System.Net.Sockets;

namespace OrderService.DataAccess.Models
{
    internal class OrderDao
    {
        public Guid Id { get; private set; }
        public int Number { get; private set; }
        public int Status { get; private set; }
        public string BuyersFullName { get; private set; }
        public string BuyersEmailAddress { get; private set; }
        public string? AddressLine { get; private set; }
        public string? PostalCode { get; private set; }
        public string? PostalOffice { get; private set; }
        public string? Country { get; private set; }

        // Constuctor for EF
        protected OrderDao()
        {

        }

        internal OrderDao(Order order)
        {
            Id = order.Id;
            Number = order.Number;
            Status = (int) order.Status;
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
            Status = (int)order.Status;
            AddressLine = order.AddressLine;
            PostalCode = order.PostalCode;
            PostalOffice = order.PostalOffice;
            Country = order.Country;
        }
    }
}