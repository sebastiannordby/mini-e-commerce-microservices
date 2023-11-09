﻿using Microsoft.Extensions.DependencyInjection;
using OrderService.DataAccess.Services;
using OrderService.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Repositories;
using OrderService.DataAccess.Repositories.Administration;
using OrderService.DataAccess.Repositories.CustomerBased;

namespace OrderService.DataAccess
{
    public static class DataAccessInstaller
    {
        public static IServiceCollection AddOrderServiceDataAccessLayer(
            this IServiceCollection services,
            Action<DbContextOptionsBuilder> efContextBuilderDelegate)
        {
            if (efContextBuilderDelegate == null)
                throw new ArgumentNullException(nameof(efContextBuilderDelegate));

            return services
                .AddDbContextFactory<OrderDbContext>(
                    efContextBuilderDelegate)
                .AddScoped<IOrderViewRepository, OrderViewRepository>()
                .AddScoped<IOrderService, OrderServiceImpl>()
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IAdminOrderViewRepository, AdminOrderViewRepository>();
        }
    }
}
