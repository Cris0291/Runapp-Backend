using ErrorOr;
using MediatR;

namespace RunnApp.Application.Reviews.Commands.DeleteReview
{
    public record DeleteReviewCommand(Guid UserId, Guid ProductId) : IRequest<ErrorOr<Success>>;
}
