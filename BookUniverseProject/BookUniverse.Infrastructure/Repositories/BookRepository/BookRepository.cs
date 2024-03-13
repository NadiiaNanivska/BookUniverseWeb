using BookUniverse.Domain.Entities;
using BookUniverse.Infrastructure.Persistence;
using BookUniverse.Infrastructure.Repositories.Base;

namespace BookUniverse.Infrastructure.Repositories.Implementation.BookRepository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(DatabaseContext context)
        : base(context)
        {
        }
    }
}
