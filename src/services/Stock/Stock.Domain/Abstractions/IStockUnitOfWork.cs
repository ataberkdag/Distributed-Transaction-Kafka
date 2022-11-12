using Core.Domain.Abstractions.Persistence;

namespace Stock.Domain.Abstractions
{
    public interface IStockUnitOfWork : IUnitOfWork
    {
        IStockRepository Stocks { get; }
    }
}
