using BookUniverse.Application.Behaviours;
using BookUniverse.Domain.Entities;
using BookUniverse.Infrastructure.Persistence;
using BookUniverse.Infrastructure.Repositories.Base.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookUniverse.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDatabaseContext(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddEntityFrameworkNpgsql().AddDbContext<DatabaseContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("DbConnectionString"))); 
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));
        }

        public static async Task IdentityConfiguration(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders();

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            List<string> roles = new List<string> { "admin", "user" };
            foreach (string role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
