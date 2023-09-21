using Microsoft.EntityFrameworkCore;
using Serilog;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Duende.IdentityServer.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IdentityService.API;

public static class SeedData
{
    public static async Task EnsureSeedData(this WebApplication app)
    {
        using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            scope.ServiceProvider.GetService<PersistedGrantDbContext>().Database.Migrate();

            var context = scope.ServiceProvider.GetService<ConfigurationDbContext>();
            context.Database.Migrate();
            EnsureSeedData(context);
            await EnsureUsers(scope);
        }
    }

    private static void EnsureSeedData(ConfigurationDbContext context)
    {
        if (!context.Clients.Any())
        {
            Log.Debug("Clients being populated");
            foreach (var client in Config.Clients.ToList())
            {
                context.Clients.Add(client.ToEntity());
            }
            context.SaveChanges();
        }
        else
        {
            Log.Debug("Clients already populated");
        }

        if (!context.IdentityResources.Any())
        {
            Log.Debug("IdentityResources being populated");
            foreach (var resource in Config.IdentityResources.ToList())
            {
                context.IdentityResources.Add(resource.ToEntity());
            }
            context.SaveChanges();
        }
        else
        {
            Log.Debug("IdentityResources already populated");
        }

        if (!context.ApiScopes.Any())
        {
            Log.Debug("ApiScopes being populated");
            foreach (var resource in Config.ApiScopes.ToList())
            {
                context.ApiScopes.Add(resource.ToEntity());
            }
            context.SaveChanges();
        }
        else
        {
            Log.Debug("ApiScopes already populated");
        }

        if (!context.IdentityProviders.Any())
        {
            Log.Debug("OIDC IdentityProviders being populated");
            context.IdentityProviders.Add(new OidcProvider
            {
                Scheme = "demoidsrv",
                DisplayName = "IdentityServer",
                Authority = "https://demo.duendesoftware.com",
                ClientId = "login",
            }.ToEntity());
            context.SaveChanges();
        }
        else
        {
            Log.Debug("OIDC IdentityProviders already populated");
        }
    }

    private static async Task EnsureUsers(IServiceScope scope)
    {
        var userManager = scope.ServiceProvider
            .GetRequiredService<UserManager<IdentityUser>>();
        
        var adminUser = await userManager
            .FindByNameAsync("admin");

        if (adminUser == null)
        {
            adminUser = new IdentityUser
            {
                UserName = "admin",
                Email = "admin@email.com",
                EmailConfirmed = true,
            };
            
            var result = await userManager.CreateAsync(adminUser, "Admin123$");
            if (!result.Succeeded)
                throw new Exception(result.Errors.First().Description);

            result = await userManager.AddClaimsAsync(adminUser, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, "Admin Adminson"),
                new Claim(JwtClaimTypes.GivenName, "Admin"),
                new Claim(JwtClaimTypes.Email, adminUser.Email),

            });

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            Log.Debug("Admin User Created");
        }
    }
}
