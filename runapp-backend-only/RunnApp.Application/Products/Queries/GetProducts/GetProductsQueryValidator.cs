using FluentValidation;

namespace RunnApp.Application.Products.Queries.GetProducts
{
    public class GetProductsQueryValidator : AbstractValidator<GetProductsQuery>
    {
        public GetProductsQueryValidator()
        {
            RuleFor(x => x.Search).NotEmpty().NotNull();
        }
    }
}
