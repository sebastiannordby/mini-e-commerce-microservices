using ProductService.Domain.Models;
using ProductService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.Repositories
{
    public interface IProductPurchaseStatsRepository
    {
        Task<List<ProductView>> GetTopTenProductsByUser(string userEmail);
        Task<List<ProductView>> GetTopTenProducts();

        Task InsertOrUpdateAsync(
            string buyersEmailAddress,
            Guid productId,
            int quantity);
    }
}
