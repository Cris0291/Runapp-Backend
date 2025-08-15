using ErrorOr;
using MediatR;

namespace RunnApp.Application.Orders.Queries.GetOrder
{
    public record GetOrderQuery(Guid UserId) : IRequest<ErrorOr<OrderWrapperDto>>;
}
