using ErrorOr;
using MediatR;

namespace RunnApp.Application.CustomerProfiles.Queries.GetUserReviews
{
    public record GetUserReviewsQuery(Guid UserId) : IRequest<ErrorOr<List<ReviewWithProductImage>>>;
}
