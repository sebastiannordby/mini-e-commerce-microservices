using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MiniECommerce.Authentication;
using MiniECommerce.Library;
using ProductService.DataAccess;
using ProductService.Domain;
using Prometheus;

namespace ProductService.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var sqlConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(sqlConnectionString))
            throw new ArgumentException("ConnectionStrings:DefaultConnection must be provided.");

        builder.Services.AddControllers();
        builder.AddECommerceLibrary(
            consumerAssembly: typeof(ProductService.Domain.DomainInstaller).Assembly);
        builder.Services.AddProductServiceDomainLayer();
        builder.Services.AddProductServiceDataAccessLayer(efOptions =>
        {
            efOptions.UseSqlServer(sqlConnectionString);
        });

        var app = builder.Build();

        app.MigrateDatabase<ProductDbContext>();
        app.UseDummyData();

        if (app.Environment.IsDevelopment())
        {
            app.UseCors(x =>
                x.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin());
        }

        app.UseMetricServer();
        app.UseRouting();
        app.UseECommerceLibrary();
        app.MapControllers();
        app.Run();
    }
}