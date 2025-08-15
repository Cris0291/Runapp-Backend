using ErrorOr;
using MediatR;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler(IUnitOfWorkPattern unitOfWorkPattern, IProductsRepository productsRepository) : IRequestHandler<DeleteProductCommand, ErrorOr<Success>>
    {
        private readonly IUnitOfWorkPattern _unitOfWorkPattern = unitOfWorkPattern;
        private readonly IProductsRepository _productsRepository = productsRepository;
        public async Task<ErrorOr<Success>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {

           var product =  await _productsRepository.GetProduct(request.ProductId);
           if(product == null) return Error.NotFound(code: "ProductWasNotFoundWithGivenId", description: "Product was not found");

           product.DeleteProduct(request.UserId);
           await _productsRepository.DeleteProduct(product);
           int numberOfRowsDeleted =  await _unitOfWorkPattern.CommitChangesAsync();
           if (numberOfRowsDeleted == 0) throw new InvalidOperationException("Product could not be deleted");

            return Result.Success;
        }
    }
}
