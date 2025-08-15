using RunApp.Domain.Common;

namespace RunApp.Domain.ReviewAggregate.Events
{
    public record UpdateReviewEvent(Review Review, Guid ProductId, Guid CustomerProfileId, int OldRating) : IDomainEvent;
}
