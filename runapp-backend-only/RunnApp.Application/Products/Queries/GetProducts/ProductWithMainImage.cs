using RunApp.Domain.PhotoAggregate;
using RunApp.Domain.Products;

namespace RunnApp.Application.Products.Queries.GetProducts
{
    public class ProductWithMainImage
    {
        public Product Product { get; set; }
        public Photo? MainImage { get; set; }
    }
}
