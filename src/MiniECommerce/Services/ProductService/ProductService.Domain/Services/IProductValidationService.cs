using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.Services
{
    public interface IProductValidationService
    {
        Task<bool> IsNumberUnique(Guid productId, string number);
    }
}
