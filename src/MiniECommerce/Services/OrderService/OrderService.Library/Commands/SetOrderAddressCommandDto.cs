using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Library.Commands
{
    public sealed class SetOrderAddressCommandDto
    {
        public Guid OrderId { get; set; }
        public string AddressLine { get; set; }
        public string PostalCode { get; set; }
        public string PostalOffice { get; set; }
        public string Country { get; set; }

        public SetOrderAddressCommandDto()
        {

        }

        public SetOrderAddressCommandDto(
            Guid orderId,
            string addressLine,
            string postalCode,
            string postalOffice,
            string country)
        {
            OrderId = orderId;
            AddressLine = addressLine;
            PostalCode = postalCode;
            PostalOffice = postalOffice;
            Country = country;
        }
    }
}
