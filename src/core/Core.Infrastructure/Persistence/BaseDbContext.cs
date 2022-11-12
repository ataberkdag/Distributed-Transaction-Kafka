using Core.Domain.Base;
using Core.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Persistence
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions opt) : base(opt)
        {

        }

        public DbSet<OutboxMessage> OutboxMessage { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        }
    }
}
