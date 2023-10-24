namespace UserService.Library
{
    public class UserInfoView
    {
        public required string Email { get; set; }
        public required string FullName { get; set; }

        public required string DeliveryAddress { get; set; }
        public required string DeliveryAddressPostalCode { get; set; }
        public required string DeliveryAddressPostalOffice { get; set; }
        public required string DeliveryAddressCountry { get; set; }

        public required string InvoiceAddress { get; set; }
        public required string InvoicePostalCode { get; set; }
        public required string InvoicePostalOffice { get; set; }
        public required string InvoiceAddressCountry { get; set; }
    }
}