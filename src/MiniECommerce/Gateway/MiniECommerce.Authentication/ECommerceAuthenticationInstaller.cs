using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

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
                    options.Authority = "https://localhost:44335";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
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