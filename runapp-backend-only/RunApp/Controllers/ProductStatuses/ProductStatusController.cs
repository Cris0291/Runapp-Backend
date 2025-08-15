using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RunApp.Api.Routes;
using RunApp.Api.Services;
using RunnApp.Application.ProductStatuses.Commands.AddOrRemoveDislike;
using RunnApp.Application.ProductStatuses.Commands.AddOrRemoveLike;

namespace RunApp.Api.Controllers.ProductStatuses
{
   
    public class ProductStatusController(ISender mediator) : ApiController
    {
        private readonly ISender _mediator = mediator;

        [Authorize]
        [HttpPost(ApiEndpoints.Products.AddOrRemoveProductLike)]
        public async Task<IActionResult> AddOrRemoveProductLike([FromRoute] Guid id, [FromQuery] bool added)
        {
            Guid userId = HttpContext.GetUserId();
            var productStatusCommand = new AddOrRemoveLikeCommand(id, userId, Like: added);

            var result = await _mediator.Send(productStatusCommand);

            return result.MatchFirst((value) => Ok(), Problem);
        }
        [Authorize]
        [HttpPost(ApiEndpoints.Products.AddOrRemoveProductDislike)]
        public async Task<IActionResult> AddOrRemoveProductDislike([FromRoute] Guid id, [FromQuery] bool added)
        {
            Guid userId = HttpContext.GetUserId();
            var productStatusCommand = new AddOrRemoveDislikeCommand(id, userId, Dislike: added);

            var result = await _mediator.Send(productStatusCommand);

            return result.MatchFirst(value => Ok(), Problem)
;        }
    }
}
