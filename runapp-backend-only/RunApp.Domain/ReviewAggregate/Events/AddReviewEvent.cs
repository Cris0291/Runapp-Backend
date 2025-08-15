using RunApp.Domain.Common;

namespace RunApp.Domain.ReviewAggregate.Events
{
    public record AddReviewEvent(Review Review, Guid ProductId, Guid customerProfileId) : IDomainEvent;
}
