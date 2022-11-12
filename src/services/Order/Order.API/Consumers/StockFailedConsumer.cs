using Core.Application.Services;
using MediatR;
using Messages;
using Messages.Events;
using Order.Application.Features.Commands;
using System.Text.Json;

namespace Order.API.Consumers
{
    public class StockFailedConsumer : BackgroundService
    {
        private readonly IEventConsumer _consumer;
        private readonly IServiceProvider _serviceProvider;

        public StockFailedConsumer(IEventConsumer consumer, IServiceProvider serviceProvider)
        {
            _consumer = consumer;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await this._consumer.ConsumeEvent(KafkaConsts.StockFailedTopicName, async (message) =>
            {
                var stockFailed = JsonSerializer.Deserialize<StockFailedIE>(message);

                using (var scope = this._serviceProvider.CreateScope())
                {
                    var mediator = scope.ServiceProvider.GetService<IMediator>();

                    await mediator.Send(new OrderFailed.Command
                    {
                        CorrelationId = stockFailed.CorrelationId
                    });
                }

            }, stoppingToken);
        }
    }
}
