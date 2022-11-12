using Core.Infrastructure.Persistence;
using Core.Infrastructure.Services.Interfaces;
using Stock.Domain.Abstractions;

namespace Stock.Infrastructure.Persistence.Repositories
{
    internal class StockUnitOfWork : UnitOfWork, IStockUnitOfWork
    {
        public StockUnitOfWork(StockDbContext dbContext,
            IIntegrationEventBuilder integrationEventBuilder,
            IServiceProvider serviceProvider)
            : base(dbContext, integrationEventBuilder)
        {
            Stocks = (IStockRepository)serviceProvider.GetService(typeof(IStockRepository));
        }

        public IStockRepository Stocks { get; set; }
    }
}
