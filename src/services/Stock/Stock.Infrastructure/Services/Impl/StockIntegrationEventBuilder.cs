using Core.Domain.Abstractions;
using Core.Infrastructure.Services.Impl;
using Core.Infrastructure.Services.Interfaces;
using Messages;
using Messages.Events;
using Stock.Domain.Events;

namespace Stock.Infrastructure.Services.Impl
{
    public class StockIntegrationEventBuilder : BaseIntegrationEventBuilder, IIntegrationEventBuilder
    {
        public override string GetTopicName(IDomainEvent domainEvent)
        {
            var result = string.Empty;

            if (domainEvent is StockDecreased or StockFailed)
                result = KafkaConsts.OrderConsumerTopicName;

            return result;
        }

        public override IntegrationEvent GetIntegrationEvent(IDomainEvent domainEvent)
        {
            var result = default(IntegrationEvent);

            if (domainEvent is StockDecreased oc)
            {
                result = new StockDecreasedIE(oc.CorrelationId, oc.UserId);
            }

            if (domainEvent is StockFailed sf)
            {
                result = new StockFailedIE(sf.CorrelationId, sf.UserId);
            }

            return result;
        }
    }
}
