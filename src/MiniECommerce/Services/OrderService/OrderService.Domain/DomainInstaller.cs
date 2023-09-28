using Microsoft.Extensions.DependencyInjection;
using OrderService.Domain.Implementation;
using OrderService.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain
{
    public static class DomainInstaller
    {
        public static IServiceCollection AddOrderServiceDomain(
            this IServiceCollection services)
        {
            return services
                .AddScoped<IOrderService, OrderingService>();
        }
    }
}
