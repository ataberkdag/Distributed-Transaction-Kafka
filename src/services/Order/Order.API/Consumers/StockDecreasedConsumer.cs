using Core.Application.Services;
using MediatR;
using Messages;
using Messages.Events;
using Order.Application.Features.Commands;
using System.Text.Json;

namespace Order.API.Consumers
{
    public class StockDecreasedConsumer : BackgroundService
    {
        private readonly IEventConsumer _consumer;
        private readonly IServiceProvider _serviceProvider;

        public StockDecreasedConsumer(IEventConsumer consumer, IServiceProvider serviceProvider)
        {
            _consumer = consumer;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await this._consumer.ConsumeEvent(KafkaConsts.StockDecreasedTopicName, async (message) =>
            {
                var orderPlaced = JsonSerializer.Deserialize<StockDecreasedIE>(message);

                using (var scope = this._serviceProvider.CreateScope())
                {
                    var mediator = scope.ServiceProvider.GetService<IMediator>();

                    await mediator.Send(new OrderCompleted.Command
                    {
                        CorrelationId = orderPlaced.CorrelationId
                    });
                }

            }, stoppingToken);
        }
    }
}
