
using Contracts.Products.Requests;
using Contracts.Products.Responses;
using RunApp.Domain.Products;
using RunnApp.Application.CustomerProfiles.Queries.GetUserLikes;
using RunnApp.Application.Products.Commands.CreateProduct;
using RunnApp.Application.Products.Commands.UpdateProduct;
using RunnApp.Application.Products.Queries.GetProducts;

namespace RunApp.Api.Mappers.Products
{
    public static class ProductMapper
    {
        public static ProductResponse ProductToProductResponse(this Product product)
        {
            IEnumerable<string> bulletpoints = product.BulletPoints.Select(bulletpoint => bulletpoint.BulletPoint);
            return new ProductResponse(product.ProductId, product.Name, product.Description, product.ActualPrice, bulletpoints, product.PriceOffer != null ? product.PriceOffer.PriceWithDiscount : null, product.PriceOffer != null ? product.PriceOffer.PromotionalText : null, product.Characteristic.Brand, product.Characteristic.Type, product.Characteristic.Color, product.Characteristic.Weight, product.Categories.Select(x => x.CategoryName).ToArray());
        }
        public static IEnumerable<ProductForCard> AllProductsToProductsResponse(this IEnumerable<ProductsJoin> productsJoin)
        {
            IEnumerable<ProductForCard> responses = productsJoin.Select(productJoin =>
            {
                if(productJoin.ProductStatus == null)
                {
                    productJoin.Product.UserLike = null;
                }
                else
                {
                    productJoin.Product.UserLike = productJoin.ProductStatus.Like;
                }
                return productJoin.Product;
            }).ToList();
            return responses;
        }

        public static CreateProductCommand ProductRequestToProductCommand(this CreateProductRequest createProduct, Guid UserId)
        {
            return new CreateProductCommand(createProduct.Name, createProduct.Description,
                createProduct.Price, createProduct.Bulletpoints,
                createProduct.PriceWithDiscount, createProduct.PromotionalText, createProduct.Characteristics.Brand, 
                createProduct.Characteristics.Type, createProduct.Characteristics.Color, createProduct.Characteristics.Weight, UserId, createProduct.Categories);
        }
        public static UpdateProductCommand ProductRequestToProductCommand(this UpdateProductRequest updateProduct, Guid productId)
        {
            return new UpdateProductCommand(updateProduct.Name, updateProduct.Description,
                updateProduct.Price, updateProduct.Bulletpoints,updateProduct.Characteristics.Brand,
                updateProduct.Characteristics.Type, updateProduct.Characteristics.Color, updateProduct.Characteristics.Weight, productId);
        }
        public static IEnumerable<UserBoughtProductsResponse> ProductsWithImageToProductsResponse(this IEnumerable<Product> productWithMainImages)
        {
            return productWithMainImages.Select(x => x.ProductWithImageToProductResponse()).ToList();
        }
        public static UserBoughtProductsResponse ProductWithImageToProductResponse(this Product productWithMainImage)
        {
            return new UserBoughtProductsResponse(productWithMainImage.ProductId, productWithMainImage.ActualPrice, productWithMainImage.Name, (productWithMainImage.Categories.Select(x => x.CategoryName).ToArray())[0]);
        }
        public static IEnumerable<CreatedProductResponseDto> ProductsWithImageToCreatedProductsResponse(this IEnumerable<ProductWithMainImage> productWithMainImages)
        {
            return productWithMainImages.Select(x => x.ProductWithImageToCreatedProductResponse()).ToList();
        }
        public static CreatedProductResponseDto ProductWithImageToCreatedProductResponse(this ProductWithMainImage productWithMainImage)
        {
            return new CreatedProductResponseDto(productWithMainImage.Product.ProductId, productWithMainImage.MainImage?.Url, productWithMainImage.Product.ActualPrice, productWithMainImage.Product.Name, productWithMainImage.Product.PriceOffer?.PriceWithDiscount, productWithMainImage.Product.Categories.Select(x => x.CategoryName).ToArray());
        }
        public static IEnumerable<ProductsWithDiscount> ProductsWithImageAndDiscountToProductsResponse(this IEnumerable<ProductWithMainImage> productWithMainImages)
        {
            return productWithMainImages.Select(x => x.ProductWithImageAndDiscountToProductResponse()).ToList();
        }
        public static ProductsWithDiscount ProductWithImageAndDiscountToProductResponse(this ProductWithMainImage productWithMainImage)
        {
            string size = productWithMainImage.Product switch
            {
                { ActualPrice: var x } when x <= 50 => "small",
                { ActualPrice: var x } when x > 50 && x <= 350 => "medium",
                { ActualPrice: var x } when x > 350 => "large",
                _ => "medium"
            };

            return new ProductsWithDiscount(productWithMainImage.Product.ProductId, productWithMainImage.Product.Name, productWithMainImage.MainImage?.Url, productWithMainImage.Product.ActualPrice, productWithMainImage.Product.PriceOffer?.Discount, productWithMainImage.Product.PriceOffer?.PriceWithDiscount, size);
        }
        public static IEnumerable<LikesWithProductResponse> LikesDtoToLikesResponse(this IEnumerable<ProductUserLikesDto> products)
        {
            return products.Select(x => x.LikeDtoToLikeResponse());
        }
        public static LikesWithProductResponse LikeDtoToLikeResponse(this ProductUserLikesDto product)
        {
            return new LikesWithProductResponse(product.Product?.ProductId, product.Product?.Name, product.Product?.ActualPrice, product.ProductStatus.ProductStatusId, product.ProductStatus.Like);
        }
        public static int[] FromQueryStarValuesToRequestQuery(this string filterByStars)
        {
            var stars = filterByStars.Split(",");
            var intStars = stars.Select(x =>
            {
                bool isInt = int.TryParse(x, out int star);
                if (!isInt) throw new InvalidOperationException("The given star did not represent a valid number");

                return star;
            }).ToArray();

            return intStars;
        }
        public static string[] FromQueryCategoryValuesToRequestQuey(this string filterByCategory)
        {
            return filterByCategory.Split(",");
        }
    }
}
