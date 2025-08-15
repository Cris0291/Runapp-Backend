using ErrorOr;
using MediatR;
using RunApp.Domain.ReviewAggregate;
using RunApp.Domain.ReviewAggregate.ReviewEnum;

namespace RunnApp.Application.Reviews.Commands.UpdateReview
{
    public record UpdateReviewCommand(Guid ProductId, Guid UserId, string Comment, int Rating, ReviewDescriptionEnums ReviewDescriptionEnums) : IRequest<ErrorOr<Review>>;
}
