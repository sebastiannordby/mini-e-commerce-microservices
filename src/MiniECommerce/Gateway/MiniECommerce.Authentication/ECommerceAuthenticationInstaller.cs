using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace MiniECommerce.Authentication
{
    public static class ECommerceAuthenticationInstaller
    {
        public static IServiceCollection AddECommerceAuthentication(
            this IServiceCollection services, ConfigurationManager configuration)
        {
            var googleClientId = configuration["Authentication:Google:ClientId"];
            if (string.IsNullOrWhiteSpace(googleClientId))
                throw new ArgumentException("Authentication:Google:ClientId must be configured.");

            var googleClientSecret = configuration["Authentication:Google:ClientSecret"];
            if (string.IsNullOrWhiteSpace(googleClientSecret))
                throw new Exception("Authentication:Google:ClientSecret must be configured");

            //services.AddAuthentication().AddGoogle(googleOptions =>
            //{
            //    googleOptions.ClientId = googleClientId;
            //    googleOptions.ClientSecret = googleClientSecret;
            //});
            services.AddCors();
            services
                .AddControllers(configure =>
                {
                    //var authenticatedUserPolicy = new AuthorizationPolicyBuilder()
                    //    .RequireAuthenticatedUser()
                    //    .Build();

                    //configure.Filters.Add(
                    //    new AuthorizeFilter(authenticatedUserPolicy));
                });

            return services;
        }
    }
}