using ErrorOr;
using MediatR;
using RunApp.Domain.Products;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.Products.Commands.AddDiscount
{
    public class AddDiscountCommandHandler(IProductsRepository productsRepository, IUnitOfWorkPattern unitOfWorkPattern) : IRequestHandler<AddDiscountCommand, ErrorOr<Success>>
    {
        private readonly IProductsRepository _productsRepository = productsRepository;
        private readonly IUnitOfWorkPattern _unitOfWorkPattern = unitOfWorkPattern;
        public async Task<ErrorOr<Success>> Handle(AddDiscountCommand request, CancellationToken cancellationToken)
        {
            Product? product = await _productsRepository.GetProduct(request.ProductId);

            if (product == null) return Error.NotFound(code: "ProductWasNotFoundWithGivenId", description: "Product was not found");

            ErrorOr<Success> wasUpdated = product.AddPriceWithDiscount(request.PriceWithDiscount, request.PromotionalText);

            if (wasUpdated.IsError) return wasUpdated.Errors;

            await _unitOfWorkPattern.CommitChangesAsync();
            return wasUpdated.Value;
        }
    }
}
