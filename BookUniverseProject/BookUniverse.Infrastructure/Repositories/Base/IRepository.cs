using System.Linq.Expressions;

namespace BookUniverse.Infrastructure.Repositories.Base
{
    public interface IRepository<T>
        where T : class
    {
        IEnumerable<T> GetAll();

        Task<T> Get(Expression<Func<T, bool>> filter);

        void Create(T entity);

        void Delete(T entity);

        void Update(T obj);
    }
}
