using Core.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Stock.Domain.Abstractions;

namespace Stock.Infrastructure.Persistence.Repositories
{
    public class StockRepository : Repository<Stock.Domain.Entities.Stock>, IStockRepository
    {
        public StockRepository(StockDbContext ctx) : base(ctx)
        {

        }
    }
}
