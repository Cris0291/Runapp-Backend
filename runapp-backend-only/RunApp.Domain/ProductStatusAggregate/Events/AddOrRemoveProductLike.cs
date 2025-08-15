using RunApp.Domain.Common;

namespace RunApp.Domain.ProductStatusAggregate.Events
{
    public record AddOrRemoveProductLike(Guid ProductId, bool added) : IDomainEvent;
}
