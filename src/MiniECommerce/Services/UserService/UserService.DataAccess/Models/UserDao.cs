namespace UserService.DataAccess.Models
{
    internal class UserDao
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }

        public string DeliveryAddress { get; set; }
        public string DeliveryAddressPostalCode { get; set; }
        public string DeliveryAddressPostalOffice { get; set; }
        public string DeliveryAddressCountry { get; set; }
        public string InvoiceAddress { get; set; }
        public string InvoicePostalCode { get; set; }
        public string InvoicePostalOffice { get; set; }
        public string InvoiceAddressCountry { get; set; }

        internal void Update(
            string deliveryAddress,
            string deliveryAddressPostalCode,
            string deliveryAddressPostalOffice,
            string deliveryAddressCountry,
            string invoiceAddress,
            string invoicePostalCode,
            string invoicePostalOffice,
            string invoiceAddressCountry)
        {
            DeliveryAddress = deliveryAddress;
            DeliveryAddressPostalCode = deliveryAddressPostalCode;
            DeliveryAddressPostalOffice = deliveryAddressPostalOffice;
            DeliveryAddressCountry = deliveryAddressCountry;
            InvoiceAddress = invoiceAddress;
            InvoicePostalCode = invoicePostalCode;
            InvoicePostalOffice = invoicePostalOffice;
            InvoiceAddressCountry = invoiceAddressCountry;
        }
    }
}
