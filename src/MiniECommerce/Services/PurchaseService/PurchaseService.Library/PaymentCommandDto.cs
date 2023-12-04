namespace PurchaseService.Library
{
    public class PaymentCommandDto
    {
        public Guid OrderId { get; set; }
        public PaymentMethod Method { get; set; }
        public CardMethod? Card { get; set; } = new();
        public VippsMethod? Vipps { get; set; } = new();

        public enum PaymentMethod
        {
            Card = 0,
            Vipps = 1
        }


        public class CardMethod
        {
            public string? CardHolderFullName { get; set; }
            public string? CardNumber { get; set; }
            public string? CVC { get; set; }
        }

        public class VippsMethod
        {
            public string? PhoneNumber { get; set; }
        }
    }
}
