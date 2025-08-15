using ErrorOr;
using MediatR;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.Products.Queries.ExistProduct
{
    public class ExistProductQueryHandler(IProductsRepository productsRepository) : IRequestHandler<ExistProductQuery, ErrorOr<Success>>
    {
        private readonly IProductsRepository _productsRepository = productsRepository;
        public async Task<ErrorOr<Success>> Handle(ExistProductQuery request, CancellationToken cancellationToken)
        {
            var result = await _productsRepository.ExistProduct(request.ProductId);

            if (!result) return Error.Validation(code: "RequestedProductWasNotFound", description: "Requested product was not found");

            return Result.Success;
        }
    }
}
