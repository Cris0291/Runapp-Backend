using Microsoft.EntityFrameworkCore;
using RunApp.Domain.CustomerProfileAggregate;
using RunApp.Domain.PhotoAggregate;
using RunApp.Domain.Products;
using RunApp.Domain.ProductStatusAggregate;
using RunApp.Domain.ReviewAggregate;
using RunApp.Infrastructure.Common.Persistence;
using RunnApp.Application.Common.Interfaces;
using RunnApp.Application.CustomerProfiles.Queries.GetUserLikes;
using RunnApp.Application.CustomerProfiles.Queries.GetUserReviews;
using RunnApp.Application.Products.Queries.GetProduct;
using RunnApp.Application.Products.Queries.GetProducts;
using System.Linq.Expressions;


namespace RunApp.Infrastructure.Common.Queries.LeftJoinQuery
{
    public class LeftJoinRepository(AppStoreDbContext appStoreDbContext) :ILeftJoinRepository
    {
        private readonly AppStoreDbContext _appStoreDbContext = appStoreDbContext;
        public IQueryable<ProductUserLikesDto> GetProductsAndStatusLeftJoin(Guid UserId)
        {
            IQueryable<ProductStatus> productStatus = _appStoreDbContext.ProductStatuses.Where(x => x.Id == UserId);
            IQueryable<Product> products = _appStoreDbContext.Products;
            Expression<Func<ProductStatus, Guid>> statusKey = (productStatus) => productStatus.ProductId;
            Expression<Func<Product, Guid>> productKey = (product) => product.ProductId;
            Expression<Func<ProductStatus, Product?, ProductUserLikesDto>> resultSelector = (productStatus, product) => new ProductUserLikesDto { Product = product, ProductStatus = productStatus };


            return productStatus.CreateOuterJoinQuery(products, statusKey, productKey, resultSelector);
            
        }
        public IQueryable<ProductsJoin> GetProductsAndStatusLeftJoin(Guid UserId, IQueryable<ProductForCard> products)
        {
            IQueryable<ProductStatus> productStatus = _appStoreDbContext.ProductStatuses.Where(x => x.Id == UserId);
            Expression<Func<ProductForCard, Guid>> productKey = (product) => product.ProductId;
            Expression<Func<ProductStatus, Guid>> statusKey = (productStatus) => productStatus.ProductId;
            Expression<Func<ProductForCard, ProductStatus?, ProductsJoin>> resultSelector = (productForCard, productStatus) => new ProductsJoin { Product = productForCard, ProductStatus = productStatus };

            return products.CreateOuterJoinQuery(productStatus, productKey, statusKey, resultSelector);
        }
        public IQueryable<ProductWithMainImage> GetProductsWithImage(IQueryable<Product> products)
        {
            IQueryable<Photo> photos = _appStoreDbContext.Photos;
            Expression<Func<Product, Guid>> productKey = (product) => product.ProductId;
            Expression<Func<Photo, Guid>> photoKey = (photo) => photo.ProductId;
            Expression<Func<Product, Photo?, ProductWithMainImage>> resultSelector = (product, photo) => new ProductWithMainImage { Product = product, MainImage = photo };

            return products.CreateOuterJoinQuery(photos, productKey, photoKey, resultSelector);
        }
        public IQueryable<ReviewAndCustomerDto> GetReviewsWithCustomer(Guid productId)
        {
            IQueryable<Review> reviews = _appStoreDbContext.Reviews.Where(x => x.ProductId == productId);
            IQueryable<CustomerProfile> customerProfiles = _appStoreDbContext.CustomerProfiles;
            Expression<Func<Review, Guid>> reviewKey = (review) => review.Id;
            Expression<Func<CustomerProfile, Guid>> customerKey = (customer) => customer.Id;
            Expression<Func<Review, CustomerProfile?, ReviewAndCustomerDto>> resultSelector = (review, customer) => new ReviewAndCustomerDto { Review = review, CustomerProfile = customer };

            return reviews.CreateOuterJoinQuery(customerProfiles, reviewKey, customerKey, resultSelector);
        }
        public IQueryable<ProductWithMainImage> GetBoughtProductWithImage(IEnumerable<Guid> boughtProducts)
        {
            var productsSet = boughtProducts.ToHashSet();
            IQueryable<Product> products = _appStoreDbContext.Products.Include(x => x.Categories).Where(x => productsSet.Contains(x.ProductId));
            IQueryable<Photo> photos = _appStoreDbContext.Photos;
            Expression<Func<Product, Guid>> productKey = (product) => product.ProductId;
            Expression<Func<Photo, Guid>> photoKey = (photo) => photo.ProductId;
            Expression<Func<Product, Photo?, ProductWithMainImage>> resultSelector = (product, photo) => new ProductWithMainImage { Product = product, MainImage = photo };

            return products.CreateOuterJoinQuery(photos, productKey, photoKey, resultSelector);
        }
        public IQueryable<ReviewWithProductImage> GetUserReviewsWithProductImage(IQueryable<ProductImageDto> products, IEnumerable<Guid> userReviews)
        {
            var reviewsSet = userReviews.ToHashSet();
            IQueryable<Review> reviews = _appStoreDbContext.Reviews.Where(x => reviewsSet.Contains(x.ProductId));
            Expression<Func<Review, Guid>> reviewKey = (review) => review.ProductId;
            Expression <Func<ProductImageDto, Guid>> productKey = (product) => product.ProductId;
            Expression<Func<Review, ProductImageDto?, ReviewWithProductImage>> resultSelector = (review, product) => new ReviewWithProductImage {Review = review, ProductImage = product };

            return reviews.CreateOuterJoinQuery(products, reviewKey, productKey, resultSelector);

        }
        public async Task<List<T>> ExecuteQuery<T>(IQueryable<T> query)
        {
            return await query.ToListAsync();
        }
    }
}
