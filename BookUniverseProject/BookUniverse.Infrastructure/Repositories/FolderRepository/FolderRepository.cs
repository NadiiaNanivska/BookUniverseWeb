using BookUniverse.Domain.Entities;
using BookUniverse.Infrastructure.Persistence;
using BookUniverse.Infrastructure.Repositories.Base;

namespace BookUniverse.Infrastructure.Repositories.Implementation.FolderRepository
{
    public class FolderRepository : Repository<Folder>, IFolderRepository
    {
        public FolderRepository(DatabaseContext context)
        : base(context)
        {
        }
    }
}
