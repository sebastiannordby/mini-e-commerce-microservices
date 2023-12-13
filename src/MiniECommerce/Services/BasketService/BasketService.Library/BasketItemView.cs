namespace BasketService.Library
{
    public class BasketItemView
    {
        public required Guid ProductId { get; set; }
        public required string ProductName { get; set; }
        public required string ProductDescription { get; set; }
        public required string ProductCategory { get; set; }
        public required int Quantity { get; set; }
        public required decimal PricePerQuantity { get; set; }
        public decimal Total => Quantity * PricePerQuantity;
    }
}