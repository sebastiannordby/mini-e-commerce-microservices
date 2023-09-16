using static System.Formats.Asn1.AsnWriter;
using System;
using Microsoft.EntityFrameworkCore;
using Duende.IdentityServer.EntityFramework.DbContexts;

namespace IdentityService.API.Data
{
    public static class DatabaseMigrationExtension
    {
        public static void MigrateDatabases(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var identityServiceDbContext = scope.ServiceProvider
                    .GetRequiredService<IdentitySeviceDbContext>();

                identityServiceDbContext.Database.Migrate();

                var configurationDbContext = scope.ServiceProvider
                    .GetRequiredService<ConfigurationDbContext>();

                configurationDbContext.Database.Migrate();

                var persistedGrantDbContext = scope.ServiceProvider
                    .GetRequiredService<PersistedGrantDbContext>();

                persistedGrantDbContext.Database.Migrate();
            }
        }
    }
}
