using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using MiniECommerce.Authentication;
using OrderService.DataAccess;
using OrderService.Domain;
using System.Diagnostics.Metrics;
using MiniECommerce.Library.Services;
using MiniECommerce.Library;

namespace OrderService.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;
        var sqlConnectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(sqlConnectionString))
            throw new ArgumentException("ConnectionStrings:DefaultConnection must be provided.");

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddOrderServiceDataAccessLayer(efOptions =>
        {
            efOptions.UseSqlServer(sqlConnectionString);
        });
        builder.Services.AddOrderServiceDomainLayer();
        builder.AddECommerceLibrary(
            consumerAssembly: typeof(OrderService.Domain.DomainInstaller).Assembly);

        var app = builder.Build();

        app.MigrateDatabase<OrderDbContext>();

        if (app.Environment.IsDevelopment())
        {
            app.UseCors(x =>
                x.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin());
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseHttpsRedirection();
        }

        app.UseRouting();
        app.UseECommerceLibrary();
        app.MapControllers();
        app.Run();
    }
}