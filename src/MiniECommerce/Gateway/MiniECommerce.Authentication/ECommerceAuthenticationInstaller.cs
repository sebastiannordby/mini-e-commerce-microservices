using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace MiniECommerce.Authentication
{
    public static class ECommerceAuthenticationInstaller
    {
        public static IServiceCollection AddECommerceAuthentication(
            this IServiceCollection services)
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://localhost:5010";
                    options.Audience = "e-commerce";
                });

            services
                .AddControllers(configure =>
                {
                    var authenticatedUserPolicy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();

                    configure.Filters.Add(
                        new AuthorizeFilter(authenticatedUserPolicy));
                });

            return services;
        }
    }
}