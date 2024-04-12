using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace BookUniverse.Infrastructure.Repositories.Base
{
    public interface IRepository<T>
        where T : class
    {
        IEnumerable<T> GetAll();

        Task<T> Get(Expression<Func<T, bool>> filter);

        T Create(T entity);

        void Delete(T entity);

        void Update(T obj);

        Task<IEnumerable<T>> GetAllAsync(
        Expression<Func<T, bool>>? predicate = default,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default);

        Task<T?> GetFirstOrDefaultAsync(
        Expression<Func<T, bool>>? predicate = default,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default);
    }
}
