using Core.Application;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Order.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            services.AddCoreApplication(Assembly.GetExecutingAssembly());
        }
    }
}
