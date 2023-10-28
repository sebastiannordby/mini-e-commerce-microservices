using MiniECommerce.Authentication;
namespace PurchaseService.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.AddECommerceLibrary();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors(x =>
                x.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin());
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