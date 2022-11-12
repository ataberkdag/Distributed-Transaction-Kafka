using Core.Domain.Abstractions.Persistence;
using Core.Domain.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.Infrastructure.Persistence
{
    public abstract class Repository<T> : IRepository<T> where T : EntityRootBase
    {
        private readonly DbContext _dbContext;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(T item)
        {
            await _dbContext.Set<T>().AddAsync(item);
        }

        public async Task<List<T>> FindByQuery(Expression<Func<T, bool>> expression, bool tracking = true)
        {
            var queryable = (IQueryable<T>)_dbContext.Set<T>();

            if (!tracking)
                queryable = queryable.AsNoTracking();

            return await queryable.Where(expression).ToListAsync();
        }

        public async Task<T> GetById(long id)
        {
            var queryable = (IQueryable<T>)_dbContext.Set<T>();

            return await _dbContext.Set<T>().FindAsync(id);
        }

        public void Remove(T item)
        {
            _dbContext.Set<T>().Remove(item);
        }
    }
}
