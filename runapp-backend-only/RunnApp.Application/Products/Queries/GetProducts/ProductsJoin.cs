using RunApp.Domain.ProductStatusAggregate;

namespace RunnApp.Application.Products.Queries.GetProducts
{
    public class ProductsJoin
    {
        public ProductForCard Product { get; set; }
        public ProductStatus? ProductStatus { get; set; }
    }
}

