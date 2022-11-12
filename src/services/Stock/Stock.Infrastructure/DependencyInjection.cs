using Core.Infrastructure;
using Core.Infrastructure.Dependencies;
using Core.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stock.Domain.Abstractions;
using Stock.Infrastructure.Persistence;
using Stock.Infrastructure.Persistence.Repositories;
using Stock.Infrastructure.Services.Impl;

namespace Stock.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            services.AddSingleton<IIntegrationEventBuilder, StockIntegrationEventBuilder>();
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IStockUnitOfWork, StockUnitOfWork>();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            services.AddDbContext<StockDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Database")));

            services.AddCoreInfrastructure();

            services.AddBrokerDependency(options => {
                options.MessageServiceType = MessageServiceType.Consumer;
                options.BrokerAddress = configuration.GetValue<string>("Kafka:BootstrapServers");
                options.ConsumerGroupId = configuration.GetValue<string>("Kafka:ConsumerGroupId");
            });

            services.AddConsulDependency(configuration);
        }
    }
}
