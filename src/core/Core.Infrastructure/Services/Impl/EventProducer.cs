using Confluent.Kafka;
using Core.Application.Services;

namespace Core.Infrastructure.Services.Impl
{
    public class EventProducer : IEventProducer
    {

        private readonly IProducer<Null, string> _producer;
        public EventProducer(IProducer<Null, string> producer)
        {
            this._producer = producer;
        }

        public async Task ProduceEvent(string topic, string message)
        {
            await this._producer.ProduceAsync(topic, new Message<Null, string>
            {
                Value = message
            });
        }
    }
}
