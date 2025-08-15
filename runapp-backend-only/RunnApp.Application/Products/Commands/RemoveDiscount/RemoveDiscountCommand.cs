using ErrorOr;
using MediatR;

namespace RunnApp.Application.Products.Commands.RemoveDiscount
{
    public record RemoveDiscountCommand(Guid ProductId) : IRequest<ErrorOr<Success>>;
}
