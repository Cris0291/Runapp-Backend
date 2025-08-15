using Contracts.Common;
using Contracts.Orders.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RunApp.Api.Mappers.Orders;
using RunApp.Api.Routes;
using RunApp.Api.Services;
using RunnApp.Application.Orders.Commands.ModifyAddress;
using RunnApp.Application.Orders.Commands.ModifyPaymentMethod;
using RunnApp.Application.Orders.Commands.PayOrder;
using RunnApp.Application.Orders.Queries.GetOrder;

namespace RunApp.Api.Controllers.Orders
{
    
    public class OrdersController(ISender mediator) : ApiController
    {
        private readonly ISender _mediator = mediator;

        [Authorize]
        [HttpPost(ApiEndpoints.Orders.Create)]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequestDto orderRequest)
        {
            Guid userId = HttpContext.GetUserId();

            var order = await _mediator.Send(orderRequest.FromOrderRequestToOrderCommand(userId));

            return order.Match(value =>
            {
                return Ok(value.FromOrderToOrderDto());
            }, Problem);
        }

        [Authorize]
        [HttpPut(ApiEndpoints.Orders.ModifyOrderAddress)]
        public async Task<IActionResult> ModifyOrderAddress([FromRoute] Guid id, [FromBody] AddressRequest address)
        {
            var result = await _mediator.Send(new ModifyAddressCommand(id, address.ZipCode, address.Address,address.City, address.Country, address.State));

            return result.Match(value => Ok(value.FromAddressToAddressDto()), Problem);
        }

        [Authorize]
        [HttpPut(ApiEndpoints.Orders.ModifyPaymentMethod)]
        public async Task<IActionResult> ModifyPaymentMethod([FromRoute]Guid id, [FromBody] CardRequest card)
        {
            var result = await _mediator.Send(new ModifyPaymentMethodCommand(id, card.CardName, card.CardNumber, card.CVV, card.ExpiryDate));
            return result.Match(value => Ok(value.FromCardToCardDto()), Problem);
        }

        [Authorize]
        [HttpPatch(ApiEndpoints.Orders.PayOrder)]
        public async Task<IActionResult> PayOrder([FromRoute] Guid id)
        {
            Guid userId = HttpContext.GetUserId();

            var result = await _mediator.Send(new PayOrderCommand(userId, id));

            return result.MatchFirst(value => Ok(), Problem);
        }

        [Authorize]
        [HttpGet(ApiEndpoints.Orders.GetCurrentOrder)]
        public async Task<IActionResult> GetOrder()
        {
            Guid userId = HttpContext.GetUserId();

            var result = await _mediator.Send(new GetOrderQuery(userId));

            return result.MatchFirst(value => Ok(value.FromOrderWrapperToOrderResponse()), Problem);
        }
    }
}
