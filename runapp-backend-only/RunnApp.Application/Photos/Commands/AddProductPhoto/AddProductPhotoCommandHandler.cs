using ErrorOr;
using MediatR;
using RunApp.Domain.PhotoAggregate;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.Photos.Commands.AddProductPhoto
{
    public class AddProductPhotoCommandHandler(IPhotoAccessor photoAccessor, IPhotoRepository photoRepository , IUnitOfWorkPattern unitOfWorkPattern) : IRequestHandler<AddProductPhotoCommand, ErrorOr<PhotoResult>>
    {
        private readonly IPhotoAccessor _photoAccessor = photoAccessor;
        private readonly IUnitOfWorkPattern _unitOfWorkPattern = unitOfWorkPattern;
        private readonly IPhotoRepository _photoRepository = photoRepository;
        public async Task<ErrorOr<PhotoResult>> Handle(AddProductPhotoCommand request, CancellationToken cancellationToken)
        {
            var photoResult = await _photoAccessor.AddPhoto(request.photo);
            if (photoResult == null) return Error.NotFound(code: "PhotoFileWasNotProvided", description: "Requested photo was not provided");

            var photo = Photo.CreatePhotoEntity(photoResult.PhotoId, photoResult.Url, request.ProductId, request.UserId);

            await _photoRepository.AddPhoto(photo);

            await _unitOfWorkPattern.CommitChangesAsync();
            return photoResult;
        }
    }
}
