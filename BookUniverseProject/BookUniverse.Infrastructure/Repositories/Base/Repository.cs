using BookUniverse.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
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

        public void Create(T entity)
        {
            _db.Add(entity);
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
    }
}
