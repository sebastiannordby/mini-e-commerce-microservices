using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MiniECommerce.Authentication.Services;

namespace MiniECommerce.Testing
{
    public class ServiceProviderBuilder
    {
        public static IServiceProvider BuildServiceProvider(Action<IServiceCollection> serviceBuilder)
        {
            var services = new ServiceCollection();
            serviceBuilder(services);
            services.AddScoped<ICurrentUserService, TestCurrentUserService>();
            return services.BuildServiceProvider();
        }
    }
}