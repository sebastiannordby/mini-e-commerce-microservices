using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
            var googleClientId = configuration["Authentication:Google:ClientId"] ?? 
                throw new Exception("Configuration: Authentication:Google:ClientId must be provided.");

            var googleClientSecret = configuration["Authentication:Google:ClientSecret"] ?? 
                throw new Exception("Configuration: Authentication:Google:ClientSecret must be provided.");

            services
                .AddAuthentication(o =>
                {
                    // This forces challenge results to be handled by Google OpenID Handler, so there's no
                    // need to add an AccountController that emits challenges for Login.
                    o.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                    
                    // This forces forbid results to be handled by Google OpenID Handler, which checks if
                    // extra scopes are required and does automatic incremental auth.
                    o.DefaultForbidScheme = GoogleDefaults.AuthenticationScheme;
                    
                    // Default scheme that will handle everything else.
                    // Once a user is authenticated, the OAuth2 token info is stored in cookies.
                    o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddGoogle(options =>
                {
                    options.ClientId = googleClientId;
                    options.ClientSecret = googleClientSecret;
                });

            services.AddAuthorization();

            services.AddCors();
            services.AddControllers();
            //services
            //    .AddControllers(configure =>
            //    {
            //        var authenticatedUserPolicy = new AuthorizationPolicyBuilder()
            //            .RequireAuthenticatedUser()
            //            .Build();

            //        configure.Filters.Add(
            //            new AuthorizeFilter(authenticatedUserPolicy));
            //    });

            return services;
        }
    }
}