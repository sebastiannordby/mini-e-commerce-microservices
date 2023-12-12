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
        Task<IEnumerable<string>> ListCategories();

        Task<IEnumerable<ProductView>?> List(
            string? searchValue = null,
            decimal? fromPricePerQuantity = null,
            decimal? toPricePerQuantity = null,
            IEnumerable<string>? categories = null);

        Task<IEnumerable<ProductView>> TopTen();

        Task<Guid> Add(ProductDto product);

        Task Update(ProductDto product);

        Task<ProductDto?> Find(Guid id);
    }
}
