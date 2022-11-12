using Confluent.Kafka;
using Core.Application.Services;

namespace Core.Infrastructure.Services.Impl
{
    public class EventConsumer : IEventConsumer
    {
        private readonly IConsumer<Null, string> _consumer;
        public EventConsumer(IConsumer<Null, string> consumer)
        {
            this._consumer = consumer;
        }

        public async Task ConsumeEvent(string topic, Func<string, Task> callback, CancellationToken cancellationToken)
        {
            this._consumer.Subscribe(topic);

            while (!cancellationToken.IsCancellationRequested)
            {
                var response = this._consumer.Consume();

                await callback(response.Message.Value);
            }
        }

        /*
        public async Task ConsumeEvent(string topic, Func<string, Task> callback, CancellationToken cancellationToken)
        {
            this._consumer.Subscribe(topic);

            var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(10));

            while (await timer.WaitForNextTickAsync(cancellationToken))
            {
                var response = this._consumer.Consume();

                await callback(response.Message.Value);
            }
        }*/
    }
}
