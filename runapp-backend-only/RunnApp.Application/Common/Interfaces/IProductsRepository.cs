using RunApp.Domain.ProductAggregate.Categories;
using RunApp.Domain.Products;
using RunnApp.Application.CustomerProfiles.Common;

namespace RunnApp.Application.Common.Interfaces
{
    public interface IProductsRepository
    {
        Task<Product?> GetProduct(Guid id);
        IQueryable<Product> GetProducts();
        Task CreateProduct(Product product);
        Task DeleteProduct(Product product);
        Task<bool> ExistProduct(Guid id);
        Task<Product> GetProductWithNoDefault(Guid id);
        IQueryable<Product> GetBoughtProducts(List<Guid> boughtProducts);
        IQueryable<Product> GetCreatedProducts(List<Guid> createdProdcucts);
        Task<Product?> GetProductWithCategories(Guid productId);
        Task<Product?> GetProductWithCategories(Guid productId, Guid categoryId);
        Task DeleteCategory(Category category);
        IQueryable<Product> GetLatestDiscounts();
        IQueryable<Product> GetProductWithCategoriesQuery(Guid productId);
    }
}
