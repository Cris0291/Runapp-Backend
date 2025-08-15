using ErrorOr;
using MediatR;
using RunApp.Domain.ProductAggregate.Categories;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.Products.Commands.AddCategory
{
    public class AddCategoryCommandHandler(IProductsRepository productsRepository, IUnitOfWorkPattern unitOfWorkPattern) : IRequestHandler<AddCategoryCommand, ErrorOr<Category>>
    {
        IProductsRepository _productsRepository = productsRepository;
        IUnitOfWorkPattern _unitOfWorkPattern = unitOfWorkPattern; 
        public async Task<ErrorOr<Category>> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var product = await _productsRepository.GetProductWithCategories(request.ProductId);
            if (product == null) return Error.NotFound(code: "ProductWasNotFoundWithGivenId", description: "Product was not found");

            var categoryOrError = product.AddCategory(request.CategoryName);
            if (categoryOrError.IsError) return categoryOrError.Errors;

            await _unitOfWorkPattern.CommitChangesAsync();
            return categoryOrError.Value;
        }
    }
}
