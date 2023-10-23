using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using MiniECommerce.Authentication.Services;

namespace MiniECommerce.Authentication
{
    public static class ECommerceAuthenticationInstaller
    {
        public static IServiceCollection AddECommerceAuthentication(
            this IServiceCollection services, ConfigurationManager configuration)
        {
            IdentityModelEventSource.ShowPII = true;

            var googleClientId = configuration["Authentication:Google:ClientId"] ?? 
                throw new Exception("Configuration: Authentication:Google:ClientId must be provided.");

            var googleClientSecret = configuration["Authentication:Google:ClientSecret"] ?? 
                throw new Exception("Configuration: Authentication:Google:ClientSecret must be provided.");


            services.AddAuthorization();
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddGoogle(options =>
                {
                    options.ClientId = googleClientId;
                    options.ClientSecret = googleClientSecret;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://accounts.google.com";
                    options.Audience = googleClientId;
                });


            //services
            //    .AddAuthentication(options =>
            //    {
            //        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //        options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            //    })
            //    .AddCookie()
            //    .AddGoogle(options =>
            //    {
            //        options.ClientId = googleClientId;
            //        options.ClientSecret = googleClientSecret;
            //        options.SaveTokens = true;
            //    });

            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddCors();
            services.AddControllers();

            return services;
        }

        public static void UseECommerceAutentication(
            this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}