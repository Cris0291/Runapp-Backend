using RunnApp.Application.Products.Queries.GetProducts;


namespace RunnApp.Application.Products.Queries.GetProduct
{
    public static class ProductItemBuilder
    {
        public static IQueryable<ProductItemDto> CreateProductItemDto(this IQueryable<ProductWithMainImage> productWithImage, List<Guid> boughtProduct, List<Guid> boughtProductWithReview, bool? liked)
        {
            return productWithImage.Select(x => new ProductItemDto {
                ProductId = x.Product.ProductId,
                Name = x.Product.Name,
                ActualPrice = x.Product.ActualPrice,
                NumberOfReviews = x.Product.NumberOfReviews,
                NumberOflikes = x.Product.NumberOfLikes,
                AverageRatings = x.Product.AverageRatings,
                PriceWithDiscount = x.Product.PriceOffer == null ? null : x.Product.PriceOffer.PriceWithDiscount,
                PromotionalText = x.Product.PriceOffer == null ? null : x.Product.PriceOffer.PromotionalText,
                Discount = x.Product.PriceOffer == null ? null : x.Product.PriceOffer.Discount,
                CategoryNames = x.Product.Categories.Select(x => x.CategoryName),
                MainImage = x.MainImage == null ? null : x.MainImage.Url,
                BulletPoints = x.Product.BulletPoints.Select(x => x.BulletPoint).ToList(),
                Reviews = null,
                Like = liked ?? false,
                IsBought = boughtProduct.Contains(x.Product.ProductId),
                IsBoughtWithReview = boughtProductWithReview.Contains(x.Product.ProductId),
            });
            
        }
        public static IQueryable<ReviewDto> CreateReviewDto(this IQueryable<ReviewAndCustomerDto> reviews)
        {
            return reviews.Select(review => new ReviewDto(review.Review.Comment, review.Review.Date, review.Review.ReviewDescription.Name, review.Review.Rating, review.Review.ReviewId, review.CustomerProfile!.Name));
        }
    }
}
