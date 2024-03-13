using BookUniverse.Infrastructure.Repositories.Implementation.BookFolderRepository;
using BookUniverse.Infrastructure.Repositories.Implementation.BookRepository;
using BookUniverse.Infrastructure.Repositories.Implementation.CategoryRepository;
using BookUniverse.Infrastructure.Repositories.Implementation.FolderRepository;
using BookUniverse.Infrastructure.Repositories.Implementation.UserBookRepository;
using BookUniverse.Infrastructure.Repositories.Implementation.UserRepository;

namespace BookUniverse.Infrastructure.Repositories.Base.UnitOfWork
{
    public interface IUnitOfWork
    {
        IBookFolderRepository BookFolderRepository { get; }

        IBookRepository BookRepository { get; }

        ICategoryRepository CategoryRepository { get; }

        IFolderRepository FolderRepository { get; }

        IUserBookRepository UserBookRepository { get; }

        IUserRepository UserRepository { get; }

        public int SaveChanges();

        public Task<int> SaveChangesAsync();
    }
}
