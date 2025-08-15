using ErrorOr;
using MediatR;
using RunApp.Domain.Products;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler(IProductsRepository productsRepository, IUnitOfWorkPattern unitOfWorkPattern) : IRequestHandler<UpdateProductCommand, ErrorOr<Product>>
    {
        private readonly IProductsRepository _productsRepository = productsRepository;
        private readonly IUnitOfWorkPattern _unitOfWorkPattern = unitOfWorkPattern;
        public async Task<ErrorOr<Product>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
           Product? product =  await _productsRepository.GetProductWithCategories(request.ProductId);

            if (product == null) return Error.NotFound(code: "ProductWasNotFoundWithGivenId", description: "Product was not found");

           ErrorOr<Success> wasUpdated = product.UpdateProduct(request.Name, request.Description, request.Price, request.Bulletpoints,
                                                               request.Brand, request.Type, request.Color, request.Weight);

            if (wasUpdated.IsError) return wasUpdated.Errors;

            await _unitOfWorkPattern.CommitChangesAsync();

            return product;
        }
    }
}
