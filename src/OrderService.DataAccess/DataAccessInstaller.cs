using Microsoft.Extensions.DependencyInjection;
using OrderService.DataAccess.Services;
using OrderService.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.DataAccess
{
    public static class DataAccessInstaller
    {
        public static IServiceCollection AddOrderServiceDataAccessLayer(
            this IServiceCollection services)
        {
            return services
                .AddScoped<IOrderService, OrderServiceImpl>();
        }
    }
}
