using Microsoft.EntityFrameworkCore;
using RunApp.Domain.ProductStatusAggregate;
using RunApp.Infrastructure.Common.Persistence;
using RunnApp.Application.Common.Interfaces;

namespace RunApp.Infrastructure.ProductStatuses.Persistence
{
    public class ProductStatusRepository(AppStoreDbContext appStoreDbContext) : IProductStatusRepository
    {
        private readonly AppStoreDbContext _appStoreDbContext = appStoreDbContext;
        public async Task AddProductStatus(ProductStatus productStatus)
        {
            await _appStoreDbContext.AddAsync(productStatus);
        }
        public async Task<ProductStatus?> GetProductStatus(Guid productId, Guid customerId)
        {
            return await _appStoreDbContext.ProductStatuses.SingleOrDefaultAsync(x => x.ProductId == productId && x.Id == customerId);
        }
        public async Task<bool> ExistProductStatus(Guid productId, Guid customerId)
        {
            return await _appStoreDbContext.ProductStatuses.AnyAsync(x => x.ProductId == productId && x.Id == customerId);
        }
        public async Task<IEnumerable<ProductStatus>> GetProductStatuses(Guid customerId)
        {
            return await _appStoreDbContext.ProductStatuses.Where(x => x.Id == customerId).ToListAsync();
        }
    }
}
