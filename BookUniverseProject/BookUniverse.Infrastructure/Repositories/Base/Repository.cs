using BookUniverse.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace BookUniverse.Infrastructure.Repositories.Base
{
    public class Repository<T> : IRepository<T>
         where T : class
    {
        private readonly DatabaseContext _db;
        internal DbSet<T> dbSet;

        public Repository(DatabaseContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }

        public T Create(T entity)
        {
            return _db.Add(entity).Entity;
        }

        public void Delete(T entity)
        {
            _db.Remove(entity);
        }

        public async Task<T> Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            return await query.FirstOrDefaultAsync();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }

        public void Update(T obj)
        {
            dbSet.Update(obj);
        }

        public async Task<IEnumerable<T>> GetAllAsync(
        Expression<Func<T, bool>>? predicate = default,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default)
        {
            return await GetQueryable(predicate, include).ToListAsync();
        }

        public async Task<T?> GetFirstOrDefaultAsync(
        Expression<Func<T, bool>>? predicate = default,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default)
        {
            return await GetQueryable(predicate, include).FirstOrDefaultAsync();
        }

        private IQueryable<T> GetQueryable(
        Expression<Func<T, bool>>? predicate = default,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default,
        Expression<Func<T, T>>? selector = default)
        {
            var query = _db.Set<T>().AsNoTracking();

            if (include is not null)
            {
                query = include(query);
            }

            if (predicate is not null)
            {
                query = query.Where(predicate);
            }

            if (selector is not null)
            {
                query = query.Select(selector);
            }

            return query.AsNoTracking();
        }
    }
}
