using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MiniECommerce.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess;

namespace UserService.Tests
{
    public class BaseUserServiceTest
    {
        public IServiceProvider Services { get; private set; }

        [SetUp]
        public void Setup()
        {
            Services = ServiceProviderBuilder.BuildServiceProvider((services) =>
            {
                services.AddUserServiceDataAccessLayer("not_defined");
                services.RemoveAll<DbContextOptions<UserDbContext>>();
                services.RemoveAll<UserDbContext>();
                services.AddDbContext<UserDbContext>(efOptions =>
                {
                    efOptions.UseInMemoryDatabase(nameof(BaseUserServiceTest), b =>
                    {
                        b.EnableNullChecks(false);
                    });
                });
            });
        }

        [TearDown]
        public void Cleanup()
        {
            var dbContext = Services
                .GetRequiredService<UserDbContext>();

            dbContext.Database.EnsureDeleted();
        }
    }
}
