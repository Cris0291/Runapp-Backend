using RunApp.Domain.Products;
using RunnApp.Application.CustomerProfiles.Queries.GetUserLikes;
using RunnApp.Application.CustomerProfiles.Queries.GetUserReviews;
using RunnApp.Application.Products.Queries.GetProduct;
using RunnApp.Application.Products.Queries.GetProducts;

namespace RunnApp.Application.Common.Interfaces
{
    public interface ILeftJoinRepository
    {
        IQueryable<ProductUserLikesDto> GetProductsAndStatusLeftJoin(Guid UserId);
        IQueryable<ProductsJoin> GetProductsAndStatusLeftJoin(Guid UserId, IQueryable<ProductForCard> products);
        IQueryable<ProductWithMainImage> GetProductsWithImage(IQueryable<Product> products);
        IQueryable<ProductWithMainImage> GetBoughtProductWithImage(IEnumerable<Guid> boughtProducts);
        IQueryable<ReviewWithProductImage> GetUserReviewsWithProductImage(IQueryable<ProductImageDto> products, IEnumerable<Guid> userReviews);
        Task<List<T>> ExecuteQuery<T>(IQueryable<T> query);
        IQueryable<ReviewAndCustomerDto> GetReviewsWithCustomer(Guid productId);
    }
}
