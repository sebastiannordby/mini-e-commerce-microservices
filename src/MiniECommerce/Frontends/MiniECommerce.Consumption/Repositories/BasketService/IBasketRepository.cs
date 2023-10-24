using BasketService.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Consumption.Repositories.BasketService
{
    public interface IBasketRepository
    {
        Task<List<BasketItemView>> AddToBasket(Guid productId);
        Task<List<BasketItemView>> IncreaseQuantity(Guid productId);
        Task<List<BasketItemView>> DecreaseQuantity(Guid productId);
    }
}
