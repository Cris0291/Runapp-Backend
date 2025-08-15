using Microsoft.Extensions.DependencyInjection;
using RunnApp.Application.Common.Interfaces;
using RunApp.Infrastructure.Products.Persistence;
using RunApp.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using RunApp.Infrastructure.Reviews.Persistence;
using RunApp.Domain.UserAggregate;
using RunApp.Domain.UserAggregate.Roles;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RunApp.Infrastructure.CustomerProfiles.Persistence;
using RunApp.Infrastructure.StoreOwnerProfiles.Persistence;
using RunApp.Infrastructure.ProductStatuses.Persistence;
using RunApp.Infrastructure.Common.Queries.LeftJoinQuery;
using RunApp.Infrastructure.Orders.Persistence;
using RunApp.Infrastructure.Photos;
using Microsoft.Extensions.Configuration;
using RunApp.Infrastructure.Photos.Persistence;
using Microsoft.Extensions.Options;

namespace RunApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString, IConfiguration configuration)
        {
            services.AddScoped<IProductsRepository, ProductRepository>();
            services.AddDbContext<AppStoreDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IUnitOfWorkPattern>(serviceProvider => serviceProvider.GetRequiredService<AppStoreDbContext>());
            services.AddScoped<IReviewsRepository, ReviewRepository>();
            services.AddScoped<ICustomerProfileRepository, CustomerProfileRepository>();
            services.AddScoped<IStoreOwnerProfileRepository, StoreOwnerProfileRepository>();
            services.AddScoped<IProductStatusRepository, ProductStatusRepository>();
            services.AddScoped<ILeftJoinRepository, LeftJoinRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPhotoAccessor, PhotoAccessor>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();
            services.Configure<CloudinarySettings>(configuration.GetSection("Cloudinary"));

            services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequiredLength = 7;
                opt.Password.RequiredUniqueChars = 1;
                opt.User.AllowedUserNameCharacters += " ";
            })
                .AddEntityFrameworkStores<AppStoreDbContext>()
                .AddRoles<AppRole>()
                .AddRoleStore<RoleStore<AppRole, AppStoreDbContext, Guid>>()
                .AddUserStore<UserStore<AppUser, AppRole, AppStoreDbContext, Guid>>();

            return services;
        }
    }
}
