using ProductService.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.DataAccess.Services
{
    internal sealed class ProductValidationService : IProductValidationService
    {
        public async Task<bool> IsNumberUnique(Guid productId, string number)
        {
            return await Task.FromResult(true); 
        }
    }
}
