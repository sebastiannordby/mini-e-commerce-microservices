namespace UserService.Library
{
    public class UserInfoView
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        
        public string DeliveryAddress { get; set; }
        public string DeliveryAddressPostalCode { get; set; }
        public string DeliveryAddressPostalOffice { get; set; }
        public string DeliveryAddressCountry { get; set; }

        public string InvoiceAddress { get; set; }
        public string InvoiceAddressPostalCode { get; set; }
        public string InvoiceAddressPostalOffice { get; set; }
        public string InvoiceAddressCountry { get; set; }

        public UserInfoView()
        {

        }

        public UserInfoView(
            string email,
            string fullName,
            string deliveryAddress,
            string deliveryAddressPostalCode,
            string deliveryAddressPostalOffice,
            string deliveryAddressCountry,
            string invoiceAddress,
            string invoiceAddressPostalCode,
            string invoiceAddressPostalOffice,
            string invoiceAddressCountry)
        {
            Email = email;
            FullName = fullName;
            DeliveryAddress = deliveryAddress;
            DeliveryAddressPostalCode = deliveryAddressPostalCode;
            DeliveryAddressPostalOffice = deliveryAddressPostalOffice;
            DeliveryAddressCountry = deliveryAddressCountry;
            InvoiceAddress = invoiceAddress;
            InvoiceAddressPostalCode = invoiceAddressPostalCode;
            InvoiceAddressPostalOffice = invoiceAddressPostalOffice;
            InvoiceAddressCountry = invoiceAddressCountry;
        }
    }
}