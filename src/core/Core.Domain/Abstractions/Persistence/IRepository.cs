using Core.Domain.Base;
using System.Linq.Expressions;

namespace Core.Domain.Abstractions.Persistence
{
    public interface IRepository<T> where T : EntityRootBase
    {
        Task AddAsync(T item);
        void Remove(T item);
        Task<List<T>> FindByQuery(Expression<Func<T, bool>> expression, bool tracking = true);
        Task<T> GetById(long id);
    }
}
