using Core.Domain.Abstractions;
using Core.Domain.Abstractions.Persistence;
using Core.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Persistence
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private readonly IIntegrationEventBuilder _integrationEventBuilder;

        public UnitOfWork(DbContext dbContext,
            IIntegrationEventBuilder integrationEventBuilder)
        {
            _dbContext = dbContext;
            _integrationEventBuilder = integrationEventBuilder;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task SaveChangesAsync()
        {
            _dbContext.DispatchDomainEvents(_integrationEventBuilder);

            await _dbContext.SaveChangesAsync();
        }
    }
}
