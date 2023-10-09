using Microsoft.Extensions.DependencyInjection;
using OrderService.Domain.Services;
using OrderService.Domain.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain
{
    public static class DomainInstaller
    {
        public static IServiceCollection AddOrderServiceDomainLayer(
            this IServiceCollection services)
        {
            return services
                .AddScoped<IInitializeOrderService, InitializeOrderService>()
                .AddScoped<ILoadOrderService, LoadOrderService>()
                .AddMediatR(options =>
                {
                    options.RegisterServicesFromAssembly(typeof(DomainInstaller).Assembly);
                });
        }
    }
}
