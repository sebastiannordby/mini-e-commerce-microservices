namespace BasketService.Library
{
    public class BasketItemView
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerQuantity { get; set; }
    }
}