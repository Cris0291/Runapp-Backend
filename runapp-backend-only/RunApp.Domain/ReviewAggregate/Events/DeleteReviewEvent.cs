using RunApp.Domain.Common;

namespace RunApp.Domain.ReviewAggregate.Events
{
    public record DeleteReviewEvent(Review Review, Guid ProductId, Guid CustomerProfileId) : IDomainEvent;
}
