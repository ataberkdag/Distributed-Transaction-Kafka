using Consul;
using Core.Infrastructure.Models;
using Core.Infrastructure.Services.Impl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Dependencies
{
    public static class ConsulDependencies
    {
        public static IServiceCollection AddConsulDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConsulConfig>(configuration.GetSection(ConsulConfig.Section));
            services.AddHostedService<ConsulRegisterService>();
            services.AddSingleton<IConsulClient, ConsulClient>(provider => new ConsulClient(option =>
            {
                option.Address = new Uri(configuration.GetValue<string>($"{ConsulConfig.Section}:Address"));
            }));
            return services;
        }
    }
}
