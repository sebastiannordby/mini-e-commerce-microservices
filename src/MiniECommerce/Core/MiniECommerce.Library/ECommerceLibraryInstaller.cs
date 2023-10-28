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
using MassTransit;
using System.Reflection;

namespace MiniECommerce.Authentication
{
    public static class ECommerceLibraryInstaller
    {
        public static IServiceCollection AddECommerceLibrary(
            this WebApplicationBuilder builder, 
            Assembly? consumerAssembly = null)
        {
            var services = builder.Services;

            builder.Host.UseSerilog((ctx, lc) => lc
                .WriteTo.Console(
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {RequestId} {Message:lj}{NewLine}{Exception}"));

            builder.AddECommerceAuthentication();
            builder.AddECommerceMessageBroker(consumerAssembly);
            builder.AddCorrelationId();
            builder.AddGatewayConsumption();
            services.AddCors();
            services.AddControllers();

            return services;
        }

        public static void AddGatewayConsumption(
            this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IGatewayProductRepository, GatewayProductRepository>();
            builder.Services.AddScoped<IGatewayBasketRepository, GatewayBasketRepository>();
            builder.Services.AddScoped<IGatewayOrderRepository, GatewayOrderRepository>();
        }

        public static void AddCorrelationId(
            this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IRequestIdService, RequestIdService>();
            builder.Services.AddScoped<OutgoingRequestHandler>();
            builder.Services.AddHttpClient<HttpClient>()
              .AddHttpMessageHandler(sp => sp.GetRequiredService<OutgoingRequestHandler>());
        }

        public static void AddECommerceAuthentication(
            this WebApplicationBuilder builder)
        {
            IdentityModelEventSource.ShowPII = true;

            var googleClientId = builder.Configuration["Authentication:Google:ClientId"] ??
                throw new Exception("Configuration: Authentication:Google:ClientId must be provided.");

            var googleClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ??
                throw new Exception("Configuration: Authentication:Google:ClientSecret must be provided.");

            builder.Services.AddScoped<AuthorizationHeaderService>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
            builder.Services.AddAuthorization();
            builder.Services
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
        }

        public static void AddECommerceMessageBroker(
            this WebApplicationBuilder builder, Assembly? consumerAssembly)
        {
            var ass = Assembly.GetExecutingAssembly().FullName;
            var messageBrokerHost = builder.Configuration["MessageBroker:Host"] ??
                throw new Exception("MessageBroker:Host must be provided");
            var messageBrokerUsername = builder.Configuration["MessageBroker:Username"] ??
                throw new Exception("MessageBroker:Host must be provided");
            var messageBrokerPassword = builder.Configuration["MessageBroker:Password"] ??
                throw new Exception("MessageBroker:Host must be provided");

            builder.Services.AddMassTransit(busConfigurator =>
            {
                if (consumerAssembly is not null)
                    busConfigurator.AddConsumers(consumerAssembly);

                busConfigurator.SetKebabCaseEndpointNameFormatter();

                busConfigurator.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(new Uri(messageBrokerHost), c =>
                    {
                        c.Username(messageBrokerUsername);
                        c.Password(messageBrokerPassword);
                    });

                    configurator.ConfigureEndpoints(context);
                });
            });
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