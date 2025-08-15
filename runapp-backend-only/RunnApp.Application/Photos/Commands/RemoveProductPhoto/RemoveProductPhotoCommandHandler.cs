using ErrorOr;
using MediatR;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.Photos.Commands.RemoveProductPhoto
{
    public class RemoveProductPhotoCommandHandler(IPhotoAccessor photoAccessor, IPhotoRepository photoRepository, IUnitOfWorkPattern unitOfWorkPattern) : IRequestHandler<RemoveProductPhotoCommand, ErrorOr<Success>>
    {
        private readonly IPhotoAccessor _photoAccessor = photoAccessor;
        private readonly IPhotoRepository _photoRepository = photoRepository;
        private readonly IUnitOfWorkPattern _unitOfWorkPattern = unitOfWorkPattern;
        public async Task<ErrorOr<Success>> Handle(RemoveProductPhotoCommand request, CancellationToken cancellationToken)
        {
            var photo = await _photoRepository.GetPhoto(request.PhotoId);
            if (photo == null) return Error.NotFound(code: "RequestedPhotoWasNotFound", description: "Requested Photo was not found");

            var result = await _photoAccessor.DeletePhoto(request.PhotoId);
            if (result.IsError) return result.Errors;

            await _photoRepository.RemovePhoto(photo);

            await _unitOfWorkPattern.CommitChangesAsync();
            return Result.Success;
        }
    }
}
