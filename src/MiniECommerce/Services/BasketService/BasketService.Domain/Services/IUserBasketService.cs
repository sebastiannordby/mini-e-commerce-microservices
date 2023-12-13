using BasketService.Library;

namespace BasketService.Domain.Services
{
    public interface IUserBasketService
    {
        Task<IEnumerable<string>> GetUsersWithBasket();
        public Task<List<BasketItemView>> GetBasket(string userEmail);
        Task<List<BasketItemView>> AddToBasket(string userEmail, Guid productId);
        Task<List<BasketItemView>> IncreaseQuantity(string userEmail, Guid productId);
        Task<List<BasketItemView>> DecreaseQuantity(string userEmail, Guid productId);
        Task ClearBasket(string userEmail);
    }
}