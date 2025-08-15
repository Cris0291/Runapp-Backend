using ErrorOr;
using MediatR;

namespace RunnApp.Application.ProductStatuses.Commands.AddOrRemoveLike
{
    public record AddOrRemoveLikeCommand(Guid ProductId, Guid UserId, bool Like) : IRequest<ErrorOr<Success>>;
}
