using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Consumption.Repositories.BasketService
{
    public interface IBasketRepository
    {
        Task AddToBasket(Guid productId);
        Task IncreaseQuantity(Guid productId);
        Task DecreaseQuantity(Guid productId);
    }
}
