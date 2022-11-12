namespace Core.Domain.Abstractions
{
    public interface IEntityRootBase
    {
        public IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    }
}
