using Core.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Stock.Infrastructure.Persistence
{
    public class StockDbContext : BaseDbContext
    {
        public StockDbContext(DbContextOptions opt) : base(opt)
        {

        }

        public DbSet<Stock.Domain.Entities.Stock> Stock { get; set; }
    }
}
