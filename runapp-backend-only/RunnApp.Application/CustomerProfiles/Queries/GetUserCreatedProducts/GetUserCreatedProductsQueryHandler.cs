using ErrorOr;
using MediatR;
using RunApp.Domain.Products;
using RunnApp.Application.Common.Interfaces;
using RunnApp.Application.Products.Queries.GetProducts;

namespace RunnApp.Application.CustomerProfiles.Queries.GetUserCreatedProducts
{
    public class GetUserCreatedProductsQueryHandler(IProductsRepository productsRepository, ICustomerProfileRepository customerProfileRepository, ILeftJoinRepository leftJoinRepository) : IRequestHandler<GetUserCreatedProductsQuery, ErrorOr<IEnumerable<ProductWithMainImage>>>
    {
        private readonly IProductsRepository _productsRepository = productsRepository;
        private readonly ICustomerProfileRepository _customerProfileRepository = customerProfileRepository;
        private readonly ILeftJoinRepository _leftJoinRepository = leftJoinRepository;
        public async Task<ErrorOr<IEnumerable<ProductWithMainImage>>> Handle(GetUserCreatedProductsQuery request, CancellationToken cancellationToken)
        {
            var user = await _customerProfileRepository.GetCustomerProfile(request.UserId);
            if (user == null) return Error.Validation(code: "UserWsNotFound", description: "User was not found");

            if (user.CreatedProducts.Count == 0) return new List<ProductWithMainImage>();

            var createdProducts =  _productsRepository.GetCreatedProducts(user.CreatedProducts);
            var createdProductsWithImage = _leftJoinRepository.GetProductsWithImage(createdProducts);

            return await _leftJoinRepository.ExecuteQuery(createdProductsWithImage);
            
        }
    }
}
