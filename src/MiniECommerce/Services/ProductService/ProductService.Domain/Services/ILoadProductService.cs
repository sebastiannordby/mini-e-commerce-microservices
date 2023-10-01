using ProductService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.Services
{
    public interface ILoadProductService
    {
        /// <summary>
        /// Allows to work on Product from existing data.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="number"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        Task<Product> LoadAsync(
            Guid id,
            string number,
            string name,
            string description,
            string category);
    }
}
