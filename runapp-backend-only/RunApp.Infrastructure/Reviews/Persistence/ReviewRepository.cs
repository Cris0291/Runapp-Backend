using Microsoft.EntityFrameworkCore;
using RunApp.Domain.ReviewAggregate;
using RunApp.Infrastructure.Common.Persistence;
using RunnApp.Application.Common.Interfaces;


namespace RunApp.Infrastructure.Reviews.Persistence
{
    public class ReviewRepository(AppStoreDbContext appStoreDbContext) : IReviewsRepository
    {
        private readonly AppStoreDbContext _appStoreDbContext = appStoreDbContext;
        public async Task<bool> ExistReview(Guid userId, Guid productId)
        {
            return await _appStoreDbContext.Reviews.AnyAsync(r => r.Id == userId && r.ProductId == productId);
        }
        public async Task<Review?> GetReview(Guid userId, Guid productId)
        {
            return await _appStoreDbContext.Reviews.SingleOrDefaultAsync(x => x.Id == userId && x.ProductId == productId);
        }
        public async Task AddReview(Review review)
        {
            await _appStoreDbContext.AddAsync(review);
        }
        public async Task RemoveReview(Review review)
        {
            _appStoreDbContext.Remove(review);
            await Task.CompletedTask;
        }
        public async Task<List<Review>> GetUserReviews(IEnumerable<Guid> userReviews, Guid userId)
        {
            var reviewsSet = userReviews.ToHashSet();
            return await _appStoreDbContext.Reviews.Where(x => reviewsSet.Contains(x.ProductId) && x.Id == userId).ToListAsync();
        }
        public async Task<bool> ExistReviewsForProduct(Guid productId)
        {
            return await _appStoreDbContext.Reviews.AnyAsync(x => x.ProductId == productId);
        }
    }
}
