using System.Net.Sockets;

namespace OrderService.DataAccess.Models
{
    public class OrderDao
    {
        public Guid Id { get; private set; }
        public int Number { get; private set; }
        public string BuyersFullName { get; private set; }
        public string AddressLine { get; private set; }
        public string PostalCode { get; private set; }
        public string PostalOffice { get; private set; }
        public string Country { get; private set; }

        public OrderDao(
            Guid id,
            int number,
            string buyersFullName,
            string addressLine,
            string postalCode,
            string postalOffice,
            string country)
        {
            Id = id;
            Number = number;
            BuyersFullName = buyersFullName;
            AddressLine = addressLine;
            PostalCode = postalCode;
            PostalOffice = postalOffice;
            Country = country;
        }
    }
}