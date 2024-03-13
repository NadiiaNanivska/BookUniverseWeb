using BookUniverse.Domain.Entities;
using BookUniverse.Infrastructure.Persistence;
using BookUniverse.Infrastructure.Repositories.Base;

namespace BookUniverse.Infrastructure.Repositories.Implementation.UserRepository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext context)
        : base(context)
        {
        }
    }
}
