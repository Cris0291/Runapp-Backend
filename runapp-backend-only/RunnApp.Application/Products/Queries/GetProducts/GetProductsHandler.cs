using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RunnApp.Application.Common.Interfaces;
using RunnApp.Application.Common.SortingPagingFiltering;

namespace RunnApp.Application.Products.Queries.GetProducts
{
    public class GetProductsHandler : IRequestHandler<GetProductsQuery, ErrorOr<IEnumerable<ProductsJoin>>>
    {
        private readonly ILeftJoinRepository _leftJoinRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly IProductStatusRepository _productStatusRepository;
        private readonly IPhotoRepository _photoRepository;
        public GetProductsHandler(ILeftJoinRepository leftJoinRepository, IProductsRepository productsRepository, IProductStatusRepository productStatusRepository, IPhotoRepository photoRepository)
        {
            _leftJoinRepository = leftJoinRepository;
            _productsRepository = productsRepository;
            _productStatusRepository = productStatusRepository;
            _photoRepository = photoRepository;
        }
        public async Task<ErrorOr<IEnumerable<ProductsJoin>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var filterMappingValues = new FilterMappingValues(request.Stars, request.Categories, request.PriceRange, request.Search);
            var filterMappingOptions = GetFilterOptions(filterMappingValues);


            var products = await _productsRepository.GetProducts().ToListAsync();

            var productsQuery = products.TransformProductWithImageQuery()
                                        .AddFiltering(filterMappingValues, filterMappingOptions);
                                                   

            var orderProducts = productsQuery.AddSortingBy(request.OrderByOptions);


            var photos = await _photoRepository.GetPhotos();
            var productWithImage = orderProducts.CreateProductWithImage(photos);
            

            var statuses = await _productStatusRepository.GetProductStatuses(request.UserId);

            return productWithImage.CreateProductWithStatus(statuses);
        }
        public IEnumerable<FilterByOptions> GetFilterOptions(FilterMappingValues filterValues)
        {
            List<FilterByOptions> filterOptions = new();
            var filterType = typeof(FilterMappingValues);
            var properties = filterType.GetProperties();

            foreach (var property in properties)
            {
                if (property.Name == "Stars" && filterValues.Stars == null) continue;
                if (property.Name == "Categories" && filterValues.Categories == null) continue;

                if (!Enum.TryParse(property.Name, out FilterByOptions option)) throw new ArgumentException("Filter option and value did not matched");
                filterOptions.Add(option);
            }

            return filterOptions;
        }
    }
}
