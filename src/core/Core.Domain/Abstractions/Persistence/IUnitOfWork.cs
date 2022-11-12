namespace Core.Domain.Abstractions.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChangesAsync();
    }
}
