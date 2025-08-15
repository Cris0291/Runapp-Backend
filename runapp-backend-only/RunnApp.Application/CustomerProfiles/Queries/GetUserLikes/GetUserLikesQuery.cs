using MediatR;

namespace RunnApp.Application.CustomerProfiles.Queries.GetUserLikes
{
    public record GetUserLikesQuery(Guid UserId) : IRequest<IEnumerable<ProductUserLikesDto>>;
}
