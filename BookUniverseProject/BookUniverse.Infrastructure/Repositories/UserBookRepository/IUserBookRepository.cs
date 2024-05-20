using BookUniverse.Domain.Entities;
using BookUniverse.Infrastructure.Repositories.Base;
using System.Linq.Expressions;

namespace BookUniverse.Infrastructure.Repositories.Implementation.UserBookRepository
{
    public interface IUserBookRepository : IRepository<UserBook>
    {
        IEnumerable<Book> GetAllByUser(Expression<Func<UserBook, bool>> filter);
        Task<UserBook> GetByUserIdAndBookIdAsync(string userId, int bookId);
    }
}
