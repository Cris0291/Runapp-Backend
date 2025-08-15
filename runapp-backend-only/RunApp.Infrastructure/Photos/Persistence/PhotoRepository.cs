using Microsoft.EntityFrameworkCore;
using RunApp.Domain.PhotoAggregate;
using RunApp.Infrastructure.Common.Persistence;
using RunnApp.Application.Common.Interfaces;

namespace RunApp.Infrastructure.Photos.Persistence
{
    public class PhotoRepository(AppStoreDbContext appStoreDbContext) : IPhotoRepository
    {
        private readonly AppStoreDbContext _appStoreDbContext = appStoreDbContext;

        public async Task AddPhoto(Photo photo)
        {
            await _appStoreDbContext.AddAsync(photo);
        }

        public async Task<Photo?> GetPhoto(string photoId)
        {
            return await _appStoreDbContext.Photos.SingleOrDefaultAsync(x => x.PhotoId == photoId);
        }

        public async Task RemovePhoto(Photo photo)
        {
            _appStoreDbContext.Remove(photo);
            await Task.CompletedTask;
        }
        public async Task<Photo?> GetPhotoForProduct(Guid productId)
        {
            return await _appStoreDbContext.Photos.Where(x => x.ProductId == productId).SingleOrDefaultAsync();
        }
        public async Task<IEnumerable<Photo>> GetPhotos()
        {
            return await _appStoreDbContext.Photos.ToListAsync();
        }
    }
}
