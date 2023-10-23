using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MiniECommerce.Authentication;
using MiniECommerce.Library;
using ProductService.DataAccess;
using ProductService.Domain;

namespace ProductService.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddECommerceAuthentication(builder.Configuration);
        builder.Services.AddProductServiceDomainLayer();
        builder.Services.AddProductServiceDataAccessLayer(efOptions =>
        {
            efOptions.UseInMemoryDatabase(nameof(ProductService), b => {
                b.EnableNullChecks(false);
            });
        });

        builder.Services.AddHostedService<RabbitMQSubscriber>();
        builder.Services.AddScoped<RabbitMQPublisher>();

        var app = builder.Build();

        app.UseDummyData();

        if (app.Environment.IsDevelopment())
        {
            app.UseCors(x =>
                x.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin());
        }

        app.UseRouting();
        app.UseECommerceAutentication();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        app.Run();
    }
}