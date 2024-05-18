using BookUniverse.Application.Behaviours;
using BookUniverse.Domain.Entities;
using BookUniverse.Infrastructure.Persistence;
using BookUniverse.Infrastructure.Repositories.Base.UnitOfWork;
using BookUniverse.Infrastructure.Services.EmailSender;
using BookUniverse.Infrastructure.Services.SearchBook;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Net.Mail;
using System.Net;
using System.Reflection;
using Microsoft.Extensions.Configuration;

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

        public static void AddServices(this IServiceCollection services, ConfigurationManager configuration)
        {
            Assembly[] currentAssemblies = AppDomain.CurrentDomain.GetAssemblies(); 
            Assembly applicationAssembly = typeof(LoggingPipelineBehavior<,>).Assembly;
            services.AddAutoMapper(currentAssemblies);
            services.AddMediatR(applicationAssembly);
            services.AddScoped<ISearchBook, SearchBook>();
            services.AddTransient<IEmailSender, EmailSender>();

            var emailConfig = configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));
        }

        public static async Task IdentityConfiguration(this IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                //options.LoginPath = "/Identity/Account/Login";
                //options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

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

        public static void AddSwaggerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "BookUniverseApi", Version = "v1" });

                opt.CustomSchemaIds(x => x.FullName);
            });
        }
    }
}
