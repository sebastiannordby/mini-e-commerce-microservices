using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MiniECommerce.Authentication;
using ProductService.DataAccess;
using ProductService.Domain;

namespace ProductService.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddECommerceAuthentication(builder.Configuration);
        builder.Services.AddProductServiceDomainLayer();
        builder.Services.AddProductServiceDataAccessLayer(efOptions =>
        {
            efOptions.UseInMemoryDatabase(nameof(ProductService), b => {
                b.EnableNullChecks(false);
            });
        });

        var app = builder.Build();

        app.UseDummyData();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors(x =>
                x.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin());
        }

        app.UseCookiePolicy();
        app.UseHttpsRedirection();
        app.MapControllers();
        //app.UseAuthentication();
        //app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}