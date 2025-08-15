using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace RunnApp.Application.Photos.Commands.AddProductPhoto
{
    public record AddProductPhotoCommand(Guid ProductId, Guid UserId, IFormFile photo) : IRequest<ErrorOr<PhotoResult>>;
}
