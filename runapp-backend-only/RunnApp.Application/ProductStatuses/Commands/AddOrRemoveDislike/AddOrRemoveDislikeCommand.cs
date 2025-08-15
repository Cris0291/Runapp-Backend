using ErrorOr;
using MediatR;

namespace RunnApp.Application.ProductStatuses.Commands.AddOrRemoveDislike
{
    public record AddOrRemoveDislikeCommand(Guid ProductId, Guid UserId, bool Dislike) : IRequest<ErrorOr<Success>>;
}
