using Contracts.LineItems.Request;
using Contracts.Orders.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RunApp.Api.Mappers.Orders;
using RunApp.Api.Routes;
using RunnApp.Application.LineItems.Commands.AddItem;
using RunnApp.Application.LineItems.Commands.ChangeItemQuantity;
using RunnApp.Application.LineItems.Commands.DeleteItem;

namespace RunApp.Api.Controllers.LineItems
{
    public class LineItmesController(ISender mediator) : ApiController
    {
        private readonly ISender _mediator = mediator;

        [Authorize]
        [HttpPost(ApiEndpoints.Orders.AddItem)]
        public async Task<IActionResult> AddItem([FromBody] ItemResquestDto lineItem, [FromRoute] Guid id)
        {
            var result  = await _mediator.Send(new AddItemCommand(id, lineItem.Id, lineItem.Name, lineItem.Quantity, lineItem.Price, lineItem.PriceWithDiscount, lineItem.TotalPrice));

            return result.Match(value =>
            {
                return Ok(value.FromLineItemToLineItemDtoResponse());
            }, Problem);
        }

        [Authorize]
        [HttpDelete(ApiEndpoints.Orders.DeleteItem)]
        public async Task<IActionResult> DeleteItem([FromRoute] Guid id, [FromQuery] Guid productId)
        {
            var result = await _mediator.Send(new DeleteItemCommand(id, productId));

            return result.MatchFirst(value => Ok(), Problem);
        }

        [Authorize]
        [HttpPut(ApiEndpoints.Orders.ChangeItemQuantity)]
        public async Task<IActionResult> ChangeQuatity([FromRoute] Guid id, [FromBody] ChangeQuantityRequestDto changeQuantity)
        {
            var result = await _mediator.Send(new ChangeItemQuantityCommand(id, changeQuantity.ProductId, changeQuantity.Quantity));

            return result.Match(value => Ok(), Problem);
        }

    }
}
