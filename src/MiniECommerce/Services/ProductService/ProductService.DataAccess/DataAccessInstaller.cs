using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductService.DataAccess.Repositories;
using ProductService.DataAccess.Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.DataAccess
{
    public static class DataAccessInstaller
    {
        public static IServiceCollection AddProductDataAccessLayer(
            this IServiceCollection services,
            Action<DbContextOptionsBuilder> efContextBuilderDelegate)
        {
            if (efContextBuilderDelegate == null)
                throw new ArgumentNullException(nameof(efContextBuilderDelegate));

            return services
                .AddDbContextFactory<ProductDbContext>(
                    efContextBuilderDelegate)
                .AddScoped<IProductViewRepository, ProductViewRepository>()
                .AddScoped<IProductRepository, ProductRepository>()
                .AddScoped<ProductDbContext>();
        }
    }
}
