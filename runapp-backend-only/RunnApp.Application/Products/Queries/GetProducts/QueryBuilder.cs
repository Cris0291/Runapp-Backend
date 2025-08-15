using RunApp.Domain.PhotoAggregate;
using RunApp.Domain.Products;
using RunApp.Domain.ProductStatusAggregate;
using RunnApp.Application.Common.SortingPagingFiltering;

namespace RunnApp.Application.Products.Queries.GetProducts
{
    public static class QueryBuilder
    {
        public static IEnumerable<ProductForCard> TransformProductWithImageQuery(this IEnumerable<Product> productsWithImage)
        {
            return productsWithImage.Select(x => new ProductForCard
            {
                ProductId = x.ProductId,
                Name = x.Name,
                ActualPrice = x.ActualPrice,
                NumberOfReviews = x.NumberOfReviews,
                NumberOflikes = x.NumberOfLikes,
                AverageRatings = x.AverageRatings,
                PriceWithDiscount = x.PriceOffer == null ? null : x.PriceOffer.PriceWithDiscount,
                PromotionalText = x.PriceOffer == null ? null : x.PriceOffer.PromotionalText,
                Discount = x.PriceOffer == null ? null : x.PriceOffer.Discount,
                CategoryNames = x.Categories.Select(x => x.CategoryName),
            }); 
        }
        public static IEnumerable<ProductForCard> AddSortingBy(this IEnumerable<ProductForCard> products, OrderByOptions orderByOptions)
        {
            switch (orderByOptions)
            {
                case OrderByOptions.SimpleOrder:
                    return products.OrderByDescending(x => x.ProductId);
                case OrderByOptions.PriceDescendingOrder:
                    return products.OrderByDescending(x => x.ActualPrice);
                case OrderByOptions.PriceAscendingOrder:
                    return products.OrderBy(x => x.ActualPrice);
                case OrderByOptions.AverageRatingAscendingOrder:
                    return products.OrderBy(x => x.AverageRatings);
                case OrderByOptions.AverageRatingDescendingOrder:
                    return products.OrderByDescending(x => x.AverageRatings);
                default:
                    return products.OrderByDescending(x => x.ProductId);
            }
        }
        public static IEnumerable<ProductForCard> AddFiltering(this IEnumerable<ProductForCard> products, FilterMappingValues filterValues, IEnumerable<FilterByOptions> options)
        {
            IEnumerable<ProductForCard> newProducts = products;
            var categoriesSet = filterValues.Categories?.ToHashSet();
            var starsSet = filterValues.Stars?.ToHashSet();

            foreach (var option in options)
            {
                switch (option)
                {
                    case FilterByOptions.Categories:
                        newProducts = newProducts.Where(x => x.CategoryNames.Any(c => categoriesSet!.Contains(c)));
                        break;
                    case FilterByOptions.Search:
                        newProducts = filterValues.Search == "all" ? newProducts : newProducts = newProducts.Where(x => x.Name.Contains(filterValues.Search));
                        break;
                    case FilterByOptions.PriceRange:
                        newProducts = newProducts.Where(x => x.ActualPrice >= filterValues.PriceRange[0] && x.ActualPrice <= filterValues.PriceRange[1]);
                        break;
                    case FilterByOptions.Stars:
                        newProducts = newProducts.Where(x => starsSet!.Contains((int)Math.Round(x.AverageRatings)));
                        break;
                    default:
                        return products;
                }
            }

            return newProducts;
        }
        public static IQueryable<T> AddPaging<T>(this IQueryable<T> query, int pageSize, int pageNumZeroStart)
        {
            if (pageNumZeroStart != 0) query.Skip(pageNumZeroStart * pageSize);
            return query.Take(pageSize);
        }
        public static List<ProductsJoin> CreateProductWithStatus(this IEnumerable<ProductForCard> products, IEnumerable<ProductStatus> status)
        {
            return products
                .GroupJoin(status,
                           product => product.ProductId,
                           status => status.ProductId,
                           (o, i) => new { o, i })
                .SelectMany(x => x.i.DefaultIfEmpty(),

                (product, status) => new ProductsJoin { Product = product.o, ProductStatus = status }
                ).ToList();
        }

        public static IEnumerable<ProductForCard> CreateProductWithImage(this IEnumerable<ProductForCard> products, IEnumerable<Photo> photos)
        {
            var result =  products
                .GroupJoin(photos,
                           product => product.ProductId,
                           photo => photo.ProductId,
                           (o, i) => new { o, i })
                .SelectMany(x => x.i.DefaultIfEmpty(),

                (product, photo) => new ProductImage { Product = product.o, MainImage = photo }
                ).ToList();

            return result.Select(x =>
            {
                x.Product.MainImage = x.MainImage?.Url;
                return x.Product;
            });
        }
    }
}
