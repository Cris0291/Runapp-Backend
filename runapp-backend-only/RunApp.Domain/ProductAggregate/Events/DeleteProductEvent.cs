using RunApp.Domain.Common;

namespace RunApp.Domain.ProductAggregate.Events
{
    public record DeleteProductEvent(Guid ProductId, Guid UserId): IDomainEvent;
}
