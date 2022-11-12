using Core.Domain.Abstractions;
using Core.Domain.Base;
using Core.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Core.Infrastructure
{
    public static class Extensions
    {
        public static void DispatchDomainEvents(this DbContext dbContext, IIntegrationEventBuilder integrationEventBuilder)
        {
            var entities = dbContext.ChangeTracker.Entries<IEntityRootBase>()
                .Where(e => e.Entity.DomainEvents != null && e.Entity.DomainEvents.Any());

            var domainEvents = entities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            if (domainEvents == null || domainEvents.Count() == 0)
                return;

            var tasks = domainEvents.Select(async (domainEvent) => {

                var integrationEvent = integrationEventBuilder.GetIntegrationEvent(domainEvent);
                var topicName = integrationEventBuilder.GetTopicName(domainEvent);

                await dbContext.Set<OutboxMessage>()
                    .AddAsync(OutboxMessage.Create(integrationEvent?.GetType().AssemblyQualifiedName, 
                        JsonSerializer.Serialize(integrationEvent), topicName));
            });

            Task.WhenAll(tasks);
        }
    }
}
