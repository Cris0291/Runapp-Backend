using Microsoft.EntityFrameworkCore;
using RunApp.Domain.ProductAggregate.Categories;
using RunApp.Domain.Products;
using RunApp.Infrastructure.Common.Persistence;
using RunnApp.Application.Common.Interfaces;

namespace RunApp.Infrastructure.Products.Persistence
{
    public class ProductRepository : IProductsRepository
    {
        private readonly AppStoreDbContext _appDbContext;

        public ProductRepository(AppStoreDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task CreateProduct(Product product)
        {
           await _appDbContext.AddAsync(product);
        }

        public async Task DeleteProduct(Product product)
        {
             _appDbContext.Products.Remove(product);
              await Task.CompletedTask;
        }

        public async Task<bool> ExistProduct(Guid id)
        {
             return await _appDbContext.Products.AnyAsync(p => p.ProductId == id);
        }

        public async Task<Product?> GetProduct(Guid id)
        {
            Product? requiredProduct = await _appDbContext.Products.SingleOrDefaultAsync(product => product.ProductId == id);
            return requiredProduct;
        }

        public async Task<Product> GetProductWithNoDefault(Guid id)
        {
            Product requiredProduct = await _appDbContext.Products.SingleAsync(product => product.ProductId == id);
            return requiredProduct;
        }
        public IQueryable<Product> GetProducts()
        {
            return _appDbContext.Products.Include(x => x.Categories).AsSplitQuery();
        }
        public IQueryable<Product> GetBoughtProducts(List<Guid> boughtProducts)
        {
            var boughtProductsSet = boughtProducts.ToHashSet();

            return _appDbContext.Products.Include(x => x.Categories).Where(x => boughtProductsSet.Contains(x.ProductId));
        }
        public IQueryable<Product> GetCreatedProducts(List<Guid> createdProdcucts)
        {
            var createdProductsSet = createdProdcucts.ToHashSet();
            return _appDbContext.Products.Include(x => x.Categories).Where(x => createdProductsSet.Contains(x.ProductId));
        }
        public async Task<Product?> GetProductWithCategories(Guid productId, Guid categoryId)
        {
            return await _appDbContext.Products.Include(x => x.Categories.Where(x => x.CategoryId == categoryId)).SingleOrDefaultAsync(x => x.ProductId == productId);
        }
        public async Task<Product?> GetProductWithCategories(Guid productId)
        {
            return await _appDbContext.Products.Include(x => x.Categories).SingleOrDefaultAsync(x => x.ProductId == productId);
        }
        public IQueryable<Product> GetProductWithCategoriesQuery(Guid productId)
        {
            return _appDbContext.Products.Include(x => x.Categories).Where(x => x.ProductId == productId);
        }
        public async Task DeleteCategory(Category category)
        {
            _appDbContext.Remove(category);
            await Task.CompletedTask;
        }
        public IQueryable<Product> GetLatestDiscounts()
        {
            return _appDbContext.Products.Where(x => x.PriceOffer != null && x.PriceOffer.PriceWithDiscount != null);
        }
       
    }
}
