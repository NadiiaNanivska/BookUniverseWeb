using BookUniverse.Infrastructure.Persistence;
using BookUniverse.Infrastructure.Repositories.Implementation.BookFolderRepository;
using BookUniverse.Infrastructure.Repositories.Implementation.BookRepository;
using BookUniverse.Infrastructure.Repositories.Implementation.CategoryRepository;
using BookUniverse.Infrastructure.Repositories.Implementation.FolderRepository;
using BookUniverse.Infrastructure.Repositories.Implementation.UserBookRepository;
using BookUniverse.Infrastructure.Repositories.Implementation.UserRepository;

namespace BookUniverse.Infrastructure.Repositories.Base.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _databaseContext;

        private IBookFolderRepository _bookFolderRepository;

        private IBookRepository _bookRepository;

        private ICategoryRepository _categoryRepository;

        private IFolderRepository _folderRepository;

        private IUserBookRepository _userBookRepository;

        private IUserRepository _userRepository;

        public UnitOfWork(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IBookFolderRepository BookFolderRepository
        {
            get
            {
                if (_bookFolderRepository is null)
                {
                    _bookFolderRepository = new BookFolderRepository(_databaseContext);
                }

                return _bookFolderRepository;
            }
        }

        public IBookRepository BookRepository
        {
            get
            {
                if (_bookRepository is null)
                {
                    _bookRepository = new BookRepository(_databaseContext);
                }

                return _bookRepository;
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (_categoryRepository is null)
                {
                    _categoryRepository = new CategoryRepository(_databaseContext);
                }

                return _categoryRepository;
            }
        }

        public IFolderRepository FolderRepository
        {
            get
            {
                if (_folderRepository is null)
                {
                    _folderRepository = new FolderRepository(_databaseContext);
                }

                return _folderRepository;
            }
        }

        public IUserBookRepository UserBookRepository
        {
            get
            {
                if (_userBookRepository is null)
                {
                    _userBookRepository = new UserBookRepository(_databaseContext);
                }

                return _userBookRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository is null)
                {
                    _userRepository = new UserRepository(_databaseContext);
                }

                return _userRepository;
            }
        }

        public int SaveChanges()
        {
            return _databaseContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _databaseContext.SaveChangesAsync();
        }
    }
}
