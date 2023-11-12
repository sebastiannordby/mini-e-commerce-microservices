using ProductService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.Repositories
{
    public interface IProductPurchaseStatsRepository
    {
        Task InsertOrUpdateAsync(
            string buyersEmailAddress,
            Guid productId,
            int quantity);
    }
}
