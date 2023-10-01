using Microsoft.Extensions.DependencyInjection;
using ProductService.DataAccess.Services;
using ProductService.Domain.Services;
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
            this IServiceCollection services)
        {
            return services
                .AddScoped<IProductValidationService, ProductValidationService>();
        }
    }
}
