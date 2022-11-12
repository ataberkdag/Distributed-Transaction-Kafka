using Core.Domain.Abstractions.Persistence;

namespace Order.Domain.Abstractions
{
    public interface IOrderUnitOfWork : IUnitOfWork
    {
        public IOrderRepository Orders { get; }
    }
}
