using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.DataAccess
{
    public static class DataAccessInstaller
    {
        public static IServiceCollection AddUserServiceDataAccessLayer(
            this IServiceCollection services, string sqlConnectionString)
        {
            return services.AddDbContextFactory<UserDbContext>(x =>
            {
                x.UseSqlServer(sqlConnectionString);
            }).AddScoped<UserDbContext>();
        }
    }
}
