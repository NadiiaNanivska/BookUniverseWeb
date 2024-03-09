using BookUniverse.Domain.Entities;
using BookUniverse.Infrastructure.Persistence;
using BookUniverse.Infrastructure.Repositories.Base;

namespace BookUniverse.Infrastructure.Repositories.Implementation.CategoryRepository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(DatabaseContext context)
        : base(context)
        {
        }
    }
}
