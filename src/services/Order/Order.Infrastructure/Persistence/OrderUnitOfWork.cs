using Core.Infrastructure.Services.Interfaces;
using Order.Domain.Abstractions;

namespace Order.Infrastructure.Persistence
{
    public class OrderUnitOfWork : Core.Infrastructure.Persistence.UnitOfWork, IOrderUnitOfWork
    {
        public OrderUnitOfWork(OrderDbContext dbContext,
            IIntegrationEventBuilder integrationEventBuilder,
            IServiceProvider serviceProvider)
            : base(dbContext, integrationEventBuilder)
        {
            Orders = (IOrderRepository)serviceProvider.GetService(typeof(IOrderRepository));
        }

        public IOrderRepository Orders { get; set; }
    }
}
