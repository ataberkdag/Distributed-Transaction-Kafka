using Core.Domain.Abstractions;
using System.Text.Json.Serialization;

namespace Core.Domain.Base
{
    public abstract class EntityRootBase : EntityBase, IEntityRootBase
    {
        [JsonIgnore]
        private List<IDomainEvent> _domainEvents;

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

        protected EntityRootBase()
        {

        }

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            if (_domainEvents == null)
                _domainEvents = new List<IDomainEvent>();

            _domainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents?.Remove(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }
}
