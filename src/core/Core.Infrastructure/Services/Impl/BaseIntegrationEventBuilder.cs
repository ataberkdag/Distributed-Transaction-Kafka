using Core.Domain.Abstractions;
using Core.Infrastructure.Services.Interfaces;
using Messages;

namespace Core.Infrastructure.Services.Impl
{
    public class BaseIntegrationEventBuilder : IIntegrationEventBuilder
    {
        public virtual IntegrationEvent GetIntegrationEvent(IDomainEvent domainEvent)
        {
            return default(IntegrationEvent);
        }

        public virtual string GetTopicName(IDomainEvent domainEvent)
        {
            return string.Empty;
        }
    }
}
