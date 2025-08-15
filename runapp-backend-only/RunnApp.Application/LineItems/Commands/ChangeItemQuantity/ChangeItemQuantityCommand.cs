using ErrorOr;
using MediatR;
using RunApp.Domain.OrderAggregate.LineItems;

namespace RunnApp.Application.LineItems.Commands.ChangeItemQuantity
{
    public record ChangeItemQuantityCommand(Guid OrderId, Guid ProductId, int Quantity) : IRequest<ErrorOr<Success>>;
}
