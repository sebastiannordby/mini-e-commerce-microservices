using BasketService.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Consumption.Repositories.BasketService.Administration
{
    public interface IAdminBasketRepository
    {
        Task<IEnumerable<string>> GetUsersBaskets();
        Task<IEnumerable<BasketItemView>> Get(string userEmail);
        Task<List<BasketItemView>> AddToBasket(string userEmail, Guid productId);
        Task<List<BasketItemView>> IncreaseQuantity(string userEmail, Guid productId);
        Task<List<BasketItemView>> DecreaseQuantity(string userEmail, Guid productId);
    }
}
