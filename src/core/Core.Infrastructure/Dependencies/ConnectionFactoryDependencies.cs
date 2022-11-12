using Core.Application.Services;
using Core.Infrastructure.Services.Impl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Dependencies
{
    public static class ConnectionFactoryDependencies
    {

        public static IServiceCollection AddPostgreConnectionFactory(this IServiceCollection services, string connectionString, string dbName)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            services.AddSingleton<IDbConnectionFactory, PostgreDbConnectionFactory>(x => new PostgreDbConnectionFactory(connectionString, dbName));

            return services;
        }
    }
}
