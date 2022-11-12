using Confluent.Kafka;
using Core.Application.Services;
using Core.Infrastructure.Services.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Dependencies
{
    public static class BrokerDependencies
    {

        public static IServiceCollection AddBrokerDependency(this IServiceCollection services, Action<Options> options)
        {
            var dependencyOptions = new Options();
            options(dependencyOptions);

            if (dependencyOptions.MessageServiceType == MessageServiceType.Producer || dependencyOptions.MessageServiceType == MessageServiceType.Both)
            {
                services.AddSingleton<IProducer<Null, string>>(x => new ProducerBuilder<Null, string>(new ProducerConfig
                {
                    BootstrapServers = dependencyOptions.BrokerAddress,
                    Acks = Acks.Leader
                }).Build());

                services.AddSingleton<IEventProducer, EventProducer>();
            }

            if (dependencyOptions.MessageServiceType == MessageServiceType.Consumer || dependencyOptions.MessageServiceType == MessageServiceType.Both)
            {
                services.AddSingleton<IConsumer<Null, string>>(x => new ConsumerBuilder<Null, string>(new ConsumerConfig
                {
                    BootstrapServers = dependencyOptions.BrokerAddress,
                    AutoOffsetReset = AutoOffsetReset.Earliest,
                    GroupId = dependencyOptions.ConsumerGroupId
                }).Build());

                services.AddSingleton<IEventConsumer, EventConsumer>();
            }

            return services;
        }
    }

    public sealed class Options
    {
        public MessageServiceType MessageServiceType { get; set; }
        public string BrokerAddress { get; set; }
        public string ConsumerGroupId { get; set; }
    }

    public enum MessageServiceType
    {
        Producer = 1,
        Consumer = 2,
        Both = 3
    }
}
