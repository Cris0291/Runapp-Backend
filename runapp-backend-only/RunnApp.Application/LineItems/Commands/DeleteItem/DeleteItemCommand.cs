using ErrorOr;
using MediatR;

namespace RunnApp.Application.LineItems.Commands.DeleteItem
{
    public record DeleteItemCommand(Guid OrderId, Guid ProductId) : IRequest<ErrorOr<Success>>;
}
