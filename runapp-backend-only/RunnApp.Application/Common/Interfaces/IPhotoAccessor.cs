using ErrorOr;
using Microsoft.AspNetCore.Http;
using RunnApp.Application.Photos;

namespace RunnApp.Application.Common.Interfaces
{
    public interface IPhotoAccessor
    {
        Task<PhotoResult?> AddPhoto(IFormFile file);
        Task<ErrorOr<Success>> DeletePhoto(string publicId);
    }
}
