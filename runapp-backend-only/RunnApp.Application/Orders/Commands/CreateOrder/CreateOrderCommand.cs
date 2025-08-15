using ErrorOr;
using MediatR;
using RunApp.Domain.OrderAggregate;

namespace RunnApp.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(Guid UserId, OrderAddress? OrderAddress, OrderCard? OrderCard) : IRequest<ErrorOr<Order>>;
}
