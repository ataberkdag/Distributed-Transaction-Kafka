using Core.Domain.Abstractions.Persistence;

namespace Stock.Domain.Abstractions
{
    public interface IStockRepository : IRepository<Stock.Domain.Entities.Stock>
    {
    }
}
