using Ocelot;
using Microsoft.Extensions.Hosting;
using Ocelot.Middleware;
using Ocelot.DependencyInjection;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.WebHost.UseIISIntegration();

        builder.Host.ConfigureAppConfiguration((hostingContext, ic) =>
        {
            ic.SetBasePath(builder.Environment.ContentRootPath);
            ic.AddJsonFile("appsettings.json", true, true);
            ic.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true);
            ic.AddJsonFile("ocelot.json", false, true);
        });

        builder.Services.AddOcelot();

        var app = builder.Build();

        app.UseStaticFiles();
        await app.UseOcelot();

        app.Run();
    }
}