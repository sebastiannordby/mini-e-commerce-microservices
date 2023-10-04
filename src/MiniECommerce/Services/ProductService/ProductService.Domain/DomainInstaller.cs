﻿using Microsoft.Extensions.DependencyInjection;
using ProductService.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MediatR.Pipeline;
using ProductService.Domain.UseCases.Behaviours;
using ProductService.Domain.UseCases;
using Microsoft.Extensions.Logging;

namespace ProductService.Domain
{
    public static class DomainInstaller
    {
        public static IServiceCollection AddProductServiceDomainLayer(
            this IServiceCollection services)
        {
            return services
                .AddLogging()
                .AddMediatR(options =>
                {
                    options.RegisterServicesFromAssembly(typeof(DomainInstaller).Assembly);
                })
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>))
                .AddScoped<ILoadProductService, LoadProductService>();
        }
    }
}
