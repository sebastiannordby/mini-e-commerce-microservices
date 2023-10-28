using ProductService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Consumption.Repositories.ProductService
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductView>?> List(
            decimal? fromPricePerQuantity = null,
            decimal? toPricePerQuantity = null,
            IEnumerable<string>? categories = null);

        Task<Guid> Add(ProductDto product);
        Task Update(ProductDto product);
        Task<ProductDto?> Find(Guid id);
    }
}
