using ProductService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<ProductDto?> Find(Guid id);
        Task<Guid> Create(ProductDto product);
        Task Update(ProductDto product);
        Task<IEnumerable<string>> ListCategories();
    }
}
