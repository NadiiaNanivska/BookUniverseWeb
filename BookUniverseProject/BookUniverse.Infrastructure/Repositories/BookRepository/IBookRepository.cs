using BookUniverse.Domain.Entities;
using BookUniverse.Infrastructure.Repositories.Base;

namespace BookUniverse.Infrastructure.Repositories.Implementation.BookRepository
{
    public interface IBookRepository : IRepository<Book>
    {
    }
}
