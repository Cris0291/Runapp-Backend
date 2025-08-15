using Microsoft.EntityFrameworkCore;
using  RunApp.Domain.Products;
using RunApp.Infrastructure.Products.Persistence;
using RunApp.Infrastructure.Reviews.Persistence;
using RunnApp.Application.Common.Interfaces;
using RunApp.Domain.UserAggregate;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RunApp.Domain.UserAggregate.Roles;
using RunApp.Infrastructure.CustomerProfiles.Persistence;
using RunApp.Infrastructure.ProductStatuses.Persistence;
using RunApp.Domain.CustomerProfileAggregate;
using RunApp.Infrastructure.Sales.Persistence;
using RunApp.Infrastructure.StoreOwnerProfiles.Persistence;
using RunApp.Domain.StoreOwnerProfileAggregate;
using RunApp.Domain.Common;
using Microsoft.AspNetCore.Http;
using RunApp.Infrastructure.LogsStock.Persistence;
using RunApp.Infrastructure.Stocks.Persistence;
using RunApp.Domain.ReviewAggregate;
using RunApp.Domain.ProductStatusAggregate;
using RunApp.Domain.OrderAggregate;
using RunApp.Domain.PhotoAggregate;
using RunApp.Infrastructure.Orders.Persistence;
using RunApp.Infrastructure.LineItems.Persistence;

namespace RunApp.Infrastructure.Common.Persistence
{
    public class AppStoreDbContext: IdentityDbContext<AppUser,AppRole, Guid>, IUnitOfWorkPattern
    {
        private IHttpContextAccessor _httpContextAccessor; 
        public DbSet<Product> Products => Set<Product>();
        public DbSet<CustomerProfile> CustomerProfiles => Set<CustomerProfile>();
        public DbSet<StoreOwnerProfile> StoreOwnerProfiles => Set<StoreOwnerProfile>();
        public DbSet<Review> Reviews => Set<Review>();
        public DbSet<ProductStatus> ProductStatuses => Set<ProductStatus>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Photo> Photos => Set<Photo>();
        public AppStoreDbContext(DbContextOptions<AppStoreDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options) 
        {
            _httpContextAccessor = httpContextAccessor;
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfigurations());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerProfileConfiguration());
            modelBuilder.ApplyConfiguration(new ProductStatusConfiguration());
            modelBuilder.ApplyConfiguration(new SalesConfiguration());
            modelBuilder.ApplyConfiguration(new LogConfiguration());
            modelBuilder.ApplyConfiguration(new StoreOwnerProfileConfiguration());
            modelBuilder.ApplyConfiguration(new StockConfiguration());
            modelBuilder.ApplyConfiguration(new LineItemsConfiguration());
            modelBuilder.ApplyConfiguration(new OrdersConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public async Task<int> CommitChangesAsync()
        {
            var domainEvents = ChangeTracker.Entries<Entity>().Select(x => x.Entity.GetEvents()).SelectMany(x => x).ToList();
           if(domainEvents.Count > 0) AddDomainEventsToQueue(domainEvents);

           return await base.SaveChangesAsync();
        }

        private void AddDomainEventsToQueue(List<IDomainEvent> domainEventsToAdd)
        {
          var eventCollection =   _httpContextAccessor.HttpContext.Items.TryGetValue("DomainEvents", out var events) && events is Queue<IDomainEvent> domainEvents 
                ? domainEvents : new Queue<IDomainEvent>();

            domainEventsToAdd.ForEach(eventCollection.Enqueue);

            _httpContextAccessor.HttpContext.Items["DomainEvents"] = eventCollection;
        }
    }
}
