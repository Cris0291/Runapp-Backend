using RunApp.Domain.Common;

namespace RunApp.Domain.OrderAggregate.Events
{
    public record CreateOrderEvent(Guid UserId, Guid OrderId) : IDomainEvent;
}
