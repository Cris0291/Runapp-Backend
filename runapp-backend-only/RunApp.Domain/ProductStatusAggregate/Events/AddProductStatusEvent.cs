using RunApp.Domain.Common;

namespace RunApp.Domain.ProductStatusAggregate.Events
{
    public record AddProductStatusEvent(Guid ProductId, Guid CustomerId, Guid ProductStatusId) : IDomainEvent;
}
