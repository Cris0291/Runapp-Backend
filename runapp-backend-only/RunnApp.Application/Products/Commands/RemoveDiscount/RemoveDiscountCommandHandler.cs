using ErrorOr;
using MediatR;
using RunApp.Domain.Products;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.Products.Commands.RemoveDiscount
{
    public class RemoveDiscountCommandHandler(IProductsRepository productsRepository, IUnitOfWorkPattern unitOfWorkPattern) : IRequestHandler<RemoveDiscountCommand, ErrorOr<Success>>
    {
        private readonly IProductsRepository _productsRepository = productsRepository;
        private readonly IUnitOfWorkPattern _unitOfWorkPattern = unitOfWorkPattern;
        public async Task<ErrorOr<Success>> Handle(RemoveDiscountCommand request, CancellationToken cancellationToken)
        {
           Product? product = await _productsRepository.GetProduct(request.ProductId);
           if (product == null) return Error.NotFound(code: "ProductWasNotFoundWithGivenId", description: "Product was not found");

            product.RemovePriceWithDiscount();

           await _unitOfWorkPattern.CommitChangesAsync();

            return Result.Success;
        }
    }
}
