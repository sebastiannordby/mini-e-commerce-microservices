using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Implementation;
using UserService.DataAccess.Repositories;

namespace UserService.DataAccess
{
    public static class DataAccessInstaller
    {
        public static IServiceCollection AddUserServiceDataAccessLayer(
            this IServiceCollection services, string sqlConnectionString)
        {
            return services
                .AddScoped<IUserInfoViewRepository, UserInfoViewRepository>()
                .AddDbContextFactory<UserDbContext>(x =>
                {
                    x.UseSqlServer(sqlConnectionString);
                }).AddScoped<UserDbContext>();
        }
    }
}
