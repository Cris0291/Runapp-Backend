using Contracts.Photos.Response;
using RunnApp.Application.Photos;

namespace RunApp.Api.Mappers.Photos
{
    public static class PhotoMapper
    {
        public static PhotoResponseDto PhotoResultToPhotoResponse(this PhotoResult photoResult)
        {
            return new PhotoResponseDto(photoResult.PhotoId, photoResult.Url);
        }
    }
}
