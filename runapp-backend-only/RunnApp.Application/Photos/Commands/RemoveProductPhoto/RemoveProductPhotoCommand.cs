using ErrorOr;
using MediatR;

namespace RunnApp.Application.Photos.Commands.RemoveProductPhoto
{
    public record RemoveProductPhotoCommand(Guid ProductId, Guid CustomerProfileId, string PhotoId) : IRequest<ErrorOr<Success>>;
}
