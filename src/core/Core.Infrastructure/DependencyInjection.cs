using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Core.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCoreInfrastructure<T>(this IServiceCollection services, Action<DependencyOptions> options) where T : DbContext
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            var dependencyOptions = services.BuildServiceProvider().GetService<IOptions<DependencyOptions>>();

            services.AddDbContext<T>(options => options.UseNpgsql(dependencyOptions?.Value.ConnectionString));

            return services;
        }
    }

    public sealed class DependencyOptions
    {
        public string ConnectionString { get; set; }
    }
}
