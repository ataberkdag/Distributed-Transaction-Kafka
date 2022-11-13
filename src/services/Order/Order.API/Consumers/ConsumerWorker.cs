using Core.Application.Services;
using MediatR;
using Messages.Events;
using Messages;
using Order.Application.Features.Commands;
using System.Text.Json;

namespace Order.API.Consumers
{
    public class ConsumerWorker : BackgroundService
    {
        private readonly IEventConsumer _consumer;
        private readonly IServiceProvider _serviceProvider;

        public ConsumerWorker(IEventConsumer consumer, IServiceProvider serviceProvider)
        {
            _consumer = consumer;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await this._consumer.ConsumeEvent(KafkaConsts.OrderConsumerTopicName, async (message) =>
            {
                var baseEvent = JsonSerializer.Deserialize<IEMessageBase>(message);

                dynamic command = null;

                if (baseEvent.Type == typeof(StockDecreasedIE).AssemblyQualifiedName)
                {
                    var stockDecreasedIE = JsonSerializer.Deserialize<StockDecreasedIE>(message);
                    command = new OrderCompleted.Command
                    {
                        CorrelationId = ((StockDecreasedIE)stockDecreasedIE).CorrelationId
                    };
                }
                else if (baseEvent.Type == typeof(StockFailedIE).AssemblyQualifiedName)
                {
                    var stockFailedIE = JsonSerializer.Deserialize<StockFailedIE>(message);
                    command = new OrderFailed.Command
                    {
                        CorrelationId = ((StockFailedIE)stockFailedIE).CorrelationId
                    };
                }

                using (var scope = this._serviceProvider.CreateScope())
                {
                    var mediator = scope.ServiceProvider.GetService<IMediator>();

                    await mediator.Send(command);
                }

            }, stoppingToken);
        }
    }

    public class IEMessageBase
    {
        public string Type { get; set; }
    }
}
