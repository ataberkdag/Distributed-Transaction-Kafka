using Core.Application.Services;
using MediatR;
using Messages;
using Messages.Events;
using Stock.Application.Features.Commands;
using System.Text.Json;

namespace Stock.API.Consumers
{
    public class OrderPlacedConsumer : BackgroundService
    {
        private readonly IEventConsumer _consumer;
        private readonly IServiceProvider _serviceProvider;

        public OrderPlacedConsumer(IEventConsumer consumer, IServiceProvider serviceProvider)
        {
            _consumer = consumer;
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Task.Run(async () => {
                await this._consumer.ConsumeEvent(KafkaConsts.OrderPlacedTopicName, async (message) =>
                {
                    var orderPlaced = JsonSerializer.Deserialize<OrderPlacedIE>(message);

                    using (var scope = this._serviceProvider.CreateScope())
                    {
                        var mediator = scope.ServiceProvider.GetService<IMediator>();

                        await mediator.Send(new DecreaseStock.Command
                        {
                            UserId = orderPlaced.UserId,
                            CorrelationId = orderPlaced.CorrelationId,
                            OrderItems = orderPlaced.OrderItems.Select(x => new Domain.Dtos.OrderItemDto(x.ItemId, x.Quantity)).ToList()
                        });
                    }

                }, stoppingToken);
            });

            return Task.CompletedTask;
        }
    }
}
