using BookUniverse.Domain.Entities;
using BookUniverse.Infrastructure.Persistence;
using BookUniverse.Infrastructure.Repositories.Base;
using System.Linq.Expressions;

namespace BookUniverse.Infrastructure.Repositories.Implementation.UserBookRepository
{
    public class UserBookRepository : Repository<UserBook>, IUserBookRepository
    {
        public UserBookRepository(DatabaseContext context)
        : base(context)
        {
        }

        public IEnumerable<Book> GetAllByUser(Expression<Func<UserBook, bool>> filter)
        {
            IQueryable<UserBook> query = dbSet;
            return query.Where(filter).Select(ub => ub.Book).ToList();
        }

        public async Task<UserBook> GetByUserIdAndBookIdAsync(string userId, int bookId)
        {
            return await Get(ub => ub.UserId == userId && ub.BookId == bookId);
        }
    }
}
