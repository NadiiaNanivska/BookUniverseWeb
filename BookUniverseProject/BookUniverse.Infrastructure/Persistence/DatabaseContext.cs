using BookUniverse.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookUniverse.Infrastructure.Persistence
{
    public class DatabaseContext : IdentityDbContext<User>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
        {
        }

        public DbSet<Book> Book { get; set; }

        public DbSet<BookFolder> BookFolder { get; set; }

        public DbSet<Category> Category { get; set; }

        public DbSet<Folder> Folder { get; set; }

        public DbSet<UserBook> UserBook { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
