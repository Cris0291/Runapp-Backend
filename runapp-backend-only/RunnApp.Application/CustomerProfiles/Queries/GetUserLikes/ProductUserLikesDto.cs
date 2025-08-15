using RunApp.Domain.Products;
using RunApp.Domain.ProductStatusAggregate;

namespace RunnApp.Application.CustomerProfiles.Queries.GetUserLikes
{
    public class ProductUserLikesDto
    {
        public Product? Product { get; set; }
        public ProductStatus ProductStatus { get; set; }
    }
}
