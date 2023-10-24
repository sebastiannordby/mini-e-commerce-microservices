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
using MiniECommerce.Authentication.Middleware;
using MiniECommerce.Authentication.Services;
using MiniECommerce.Library.Services.BasketService;
using MiniECommerce.Library.Services.ProductService;
using MiniECommerce.Library.Services;
using Serilog;
using MiniECommerce.Library.Services.OrderService;

namespace MiniECommerce.Authentication
{
    public static class ECommerceLibraryInstaller
    {
        public static IServiceCollection AddECommerceLibrary(
            this WebApplicationBuilder builder, ConfigurationManager configuration)
        {
            IdentityModelEventSource.ShowPII = true;

            var googleClientId = configuration["Authentication:Google:ClientId"] ?? 
                throw new Exception("Configuration: Authentication:Google:ClientId must be provided.");

            var googleClientSecret = configuration["Authentication:Google:ClientSecret"] ?? 
                throw new Exception("Configuration: Authentication:Google:ClientSecret must be provided.");

            builder.Host.UseSerilog((ctx, lc) => lc
                .WriteTo.Console(
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {RequestId} {Message:lj}{NewLine}{Exception}"));

            var services = builder.Services;

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

            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddCors();
            services.AddTransient<IRequestIdService, RequestIdService>();
            services.AddTransient<OutgoingRequestHandler>();
            services.AddControllers();
            services.AddScoped<AuthorizationHeaderService>();
            services.AddHttpClient<HttpClient>()
              .AddHttpMessageHandler<OutgoingRequestHandler>();
            services.AddScoped<HttpClient>();

            services.AddScoped<IGatewayProductRepository, GatewayProductRepository>();
            services.AddScoped<IGatewayBasketRepository, GatewayBasketRepository>();
            services.AddScoped<IGatewayOrderRepository, GatewayOrderRepository>();

            return services;
        }

        public static void UseECommerceLibrary(
            this WebApplication app)
        {
            app.UseMiddleware<RequestLoggingMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}