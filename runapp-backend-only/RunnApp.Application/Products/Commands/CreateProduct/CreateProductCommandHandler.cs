using ErrorOr;
using MediatR;
using RunApp.Domain.Products;
using RunnApp.Application.Common.Interfaces;
using System.Reflection.Metadata.Ecma335;


namespace RunnApp.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ErrorOr<Product>>
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IUnitOfWorkPattern _unitOfWorkPattern;
        public CreateProductCommandHandler(IProductsRepository productsRepository, IUnitOfWorkPattern unitOfWorkPattern)
        {
            _productsRepository = productsRepository;
            _unitOfWorkPattern = unitOfWorkPattern;
        }
        public async Task<ErrorOr<Product>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            List<Error> errors = new();
            ErrorOr<Product> productOrError = Product.CreateProduct(request.Name, request.Description, request.Price, request.Bulletpoints, 
                                                                    request.PriceWithDiscount, request.PromotionalText, request.Brand, request.Type, request.Color, request.Weight, request.UserId);

            if (productOrError.IsError)
            {
                return productOrError.Errors;
            }

            foreach(var category in request.Categories)
            {
                var errorOrCategory = productOrError.Value.AddCategory(category);
                if (errorOrCategory.IsError) errors.Add(errorOrCategory.Errors.First());
            }
            if (errors.Count > 0) return errors;

            await _productsRepository.CreateProduct(productOrError.Value);

            int rowsChanged = await _unitOfWorkPattern.CommitChangesAsync();

            if (rowsChanged == 0 && productOrError.Value.ProductId == Guid.Empty) throw new InvalidOperationException("Product could not be added to the database");

            return productOrError.Value;
        }
    }
}
