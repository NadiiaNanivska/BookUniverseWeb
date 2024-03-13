using BookUniverse.Domain.Entities;
using BookUniverse.Infrastructure.Persistence;
using BookUniverse.Infrastructure.Repositories.Base;

namespace BookUniverse.Infrastructure.Repositories.Implementation.BookFolderRepository
{
    public class BookFolderRepository : Repository<BookFolder>, IBookFolderRepository
    {
        public BookFolderRepository(DatabaseContext context)
        : base(context)
        {
        }
    }
}
