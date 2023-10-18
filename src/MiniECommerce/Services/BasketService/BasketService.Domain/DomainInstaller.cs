using BasketService.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Domain
{
    public static class DomainInstaller
    {
        public static IServiceCollection AddBasketServiceDomainLayer(
            this IServiceCollection services)
        {
            return services
                .AddScoped<IUserBasketService, UserBasketService>();
        }
    }
}
