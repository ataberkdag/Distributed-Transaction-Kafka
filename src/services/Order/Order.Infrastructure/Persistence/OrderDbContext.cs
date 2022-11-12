using Core.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;

namespace Order.Infrastructure.Persistence
{
    public class OrderDbContext : BaseDbContext
    {
        public OrderDbContext(DbContextOptions opt) : base(opt)
        {

        }

        public DbSet<Order.Domain.Entities.Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
    }
}
