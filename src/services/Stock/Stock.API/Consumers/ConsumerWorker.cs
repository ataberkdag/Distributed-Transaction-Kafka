using Core.Application.Services;
using MediatR;
using Messages;
using Messages.Events;
using Stock.Application.Features.Commands;
using System.Text.Json;

namespace Stock.API.Consumers
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
            await this._consumer.ConsumeEvent(KafkaConsts.StockConsumerTopicName, async (message) =>
            {
                var baseEvent = JsonSerializer.Deserialize<IEMessageBase>(message);

                dynamic command = null;

                if (baseEvent.Type == typeof(OrderPlacedIE).AssemblyQualifiedName)
                {
                    var orderPlaced = JsonSerializer.Deserialize<OrderPlacedIE>(message);
                    command = new DecreaseStock.Command
                    {
                        UserId = orderPlaced.UserId,
                        CorrelationId = orderPlaced.CorrelationId,
                        OrderItems = orderPlaced.OrderItems.Select(x => new Domain.Dtos.OrderItemDto(x.ItemId, x.Quantity)).ToList()
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
