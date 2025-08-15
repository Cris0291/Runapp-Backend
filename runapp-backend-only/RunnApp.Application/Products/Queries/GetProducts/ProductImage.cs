using RunApp.Domain.PhotoAggregate;

namespace RunnApp.Application.Products.Queries.GetProducts
{
    public class ProductImage
    {
        public ProductForCard Product { get; set; }
        public Photo? MainImage { get; set; }
    }
}
