using Confluent.Kafka;
using Core.Infrastructure;
using Core.Infrastructure.Dependencies;
using Core.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Domain.Abstractions;
using Order.Infrastructure.Persistence;
using Order.Infrastructure.Persistence.Repositories;
using Order.Infrastructure.Services.Impl;

namespace Order.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            services.AddSingleton<IIntegrationEventBuilder, OrderIntegrationEventBuilder>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderUnitOfWork, OrderUnitOfWork>();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            services.AddDbContext<OrderDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Database")));

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
