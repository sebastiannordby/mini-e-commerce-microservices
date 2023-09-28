using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace MiniECommerce.Testing
{
    public class ServiceProviderBuilder
    {
        public static IServiceProvider BuildServiceProvider(Action<IServiceCollection> serviceBuilder)
        {
            var services = new ServiceCollection();
            serviceBuilder(services);

            return services.BuildServiceProvider();
        }
    }
}