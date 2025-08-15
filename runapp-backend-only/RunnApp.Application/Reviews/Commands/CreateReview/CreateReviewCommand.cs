using ErrorOr;
using MediatR;
using RunApp.Domain.ReviewAggregate;
using RunApp.Domain.ReviewAggregate.ReviewEnum;

namespace RunnApp.Application.Reviews.Commands.CreateReview
{
    public record CreateReviewCommand(Guid ProductId, Guid UserId, string Comment, int Rating,  ReviewDescriptionEnums ReviewDescriptionEnum) : IRequest<ErrorOr<Review>>;
}
