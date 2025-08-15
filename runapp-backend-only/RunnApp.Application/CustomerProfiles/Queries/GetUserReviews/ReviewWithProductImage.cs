using RunApp.Domain.ReviewAggregate;

namespace RunnApp.Application.CustomerProfiles.Queries.GetUserReviews
{
    public class ReviewWithProductImage
    {
        public ProductImageDto? ProductImage { get; set; }
        public Review Review { get; set; }
    }
}
