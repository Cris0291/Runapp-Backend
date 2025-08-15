using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RunApp.Api.Contracts;
using RunApp.Api.Routes;
using RunnApp.Application.Photos.Commands.AddProductPhoto;
using RunApp.Api.Services;
using RunApp.Api.Mappers.Photos;
using RunnApp.Application.Photos.Commands.RemoveProductPhoto;

namespace RunApp.Api.Controllers.Photos
{
    public class PhotoController(ISender mediator) : ApiController
    {
        private readonly ISender _mediator = mediator;

        [Authorize]
        [HttpPost(ApiEndpoints.Products.AddPhoto)]
        public async Task<IActionResult> AddProductPhoto([FromRoute] Guid id , [FromForm] IFormFile photoRequest)
        {
            Guid userId = HttpContext.GetUserId();
            var result = await _mediator.Send(new AddProductPhotoCommand(id, userId, photoRequest));

            return result.MatchFirst(value => Ok(value.PhotoResultToPhotoResponse()), Problem);
        }
        [Authorize]
        [HttpDelete(ApiEndpoints.Products.RemovePhoto)]
        public async Task<IActionResult> RemovePhoto([FromRoute] Guid productId, [FromRoute] string photoId)
        {
            Guid userId = HttpContext.GetUserId();
            var result = await _mediator.Send(new RemoveProductPhotoCommand(productId, userId, photoId));

            return result.MatchFirst(value => Ok(), Problem);
        }
    }
}
