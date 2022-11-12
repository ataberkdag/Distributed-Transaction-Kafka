using Core.Domain.Abstractions;
using Core.Infrastructure.Services.Impl;
using Core.Infrastructure.Services.Interfaces;
using Messages;
using Messages.Events;
using Order.Domain.Events;

namespace Order.Infrastructure.Services.Impl
{
    public class OrderIntegrationEventBuilder : BaseIntegrationEventBuilder, IIntegrationEventBuilder
    {
        public override string GetTopicName(IDomainEvent domainEvent)
        {
            var result = string.Empty;

            if (domainEvent is OrderPlaced)
                result = KafkaConsts.OrderPlacedTopicName;

            return result;
        }

        public override IntegrationEvent GetIntegrationEvent(IDomainEvent domainEvent)
        {
            var result = default(IntegrationEvent);

            if (domainEvent is OrderPlaced oc)
            {
                result = new OrderPlacedIE(oc.CorrelationId, oc.UserId, oc.OrderItems.Select(oi => new OrderItemIEDto(oi.ItemId, oi.Quantity)).ToList());
            }

            return result;
        }
    }
}
