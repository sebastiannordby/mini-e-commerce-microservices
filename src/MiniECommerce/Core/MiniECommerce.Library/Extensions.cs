using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Library
{
    public static class Extensions
    {
        public static void MigrateDatabase<TDbContext>(this WebApplication app)
            where TDbContext : DbContext
        {
            using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<TDbContext>();
                if (context is not null)
                    context.Database.Migrate();
            }
        }
    }
}
