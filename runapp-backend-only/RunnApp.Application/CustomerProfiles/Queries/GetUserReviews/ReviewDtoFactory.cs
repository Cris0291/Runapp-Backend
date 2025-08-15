using RunApp.Domain.ReviewAggregate;
using RunApp.Domain.Products;

namespace RunnApp.Application.CustomerProfiles.Queries.GetUserReviews
{
    public static class ReviewDtoFactory
    {
        public static IQueryable<ProductImageDto> FromProductsToProductsDto(this IQueryable<Product> products)
        {
            return products.Select(x => new ProductImageDto(x.ProductId, x.Name));
        }
        public static List<ReviewWithProductImage> CreateReviewsWithProduct(this List<Review> reviews, List<ProductImageDto> products)
        {
            return reviews
                .GroupJoin(products,
                           review => review.ProductId,
                           product => product.ProductId,
                           (o, i) => new { o, i })
                .SelectMany(x => x.i.DefaultIfEmpty(),
 
                (review, product) => new ReviewWithProductImage { Review = review.o, ProductImage = product }
                ).ToList();
        }
    }
}
