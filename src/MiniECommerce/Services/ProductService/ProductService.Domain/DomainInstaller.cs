using Microsoft.Extensions.DependencyInjection;
using ProductService.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain
{
    public static class DomainInstaller
    {
        public static IServiceCollection AddProductServiceDomainLayer(
            this IServiceCollection services)
        {
            return services
                .AddScoped<ILoadProductService, LoadProductService>();
        }
    }
}
