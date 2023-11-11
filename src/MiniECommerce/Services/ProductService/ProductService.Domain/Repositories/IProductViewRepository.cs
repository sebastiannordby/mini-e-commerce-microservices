using ProductService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.Repositories
{
    public interface IProductViewRepository
    {
        Task<ProductView?> Find(Guid productId);
        Task<IEnumerable<ProductView>> List(
            decimal? fromPricePerQuantity = null, 
            decimal? toPricePerQuantity = null, 
            IEnumerable<string>? categories = null);
    }
}
