using RunApp.Domain.Common;

namespace RunApp.Domain.OrderAggregate.Events
{
    public record AddBoughtProductsEvent(Guid UserId, IEnumerable<Guid> BoughtProducts) : IDomainEvent;
}
