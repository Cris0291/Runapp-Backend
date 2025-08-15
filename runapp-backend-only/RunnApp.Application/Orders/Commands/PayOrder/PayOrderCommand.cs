using ErrorOr;
using MediatR;

namespace RunnApp.Application.Orders.Commands.PayOrder
{
    public record PayOrderCommand(Guid UserId, Guid OrderId) : IRequest<ErrorOr<Success>>;
}
