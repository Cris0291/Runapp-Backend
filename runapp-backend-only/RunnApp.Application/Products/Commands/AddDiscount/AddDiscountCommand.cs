using ErrorOr;
using MediatR;
using RunApp.Domain.Products;

namespace RunnApp.Application.Products.Commands.AddDiscount
{
    public record AddDiscountCommand(Guid ProductId, decimal PriceWithDiscount, string PromotionalText) : IRequest<ErrorOr<Success>>;
}
