using Contracts.Orders.Response;
using RunApp.Domain.Common.ValueType;
using RunApp.Domain.OrderAggregate;
using RunApp.Domain.OrderAggregate.LineItems;
using Contracts.LineItems.Response;
using RunnApp.Application.Orders.Commands.CreateOrder;
using Contracts.Orders.Request;
using RunnApp.Application.Orders.Queries.GetOrder;

namespace RunApp.Api.Mappers.Orders
{
    public static class OrderMapper
    {
        public static CreateOrderCommand FromOrderRequestToOrderCommand(this OrderRequestDto orderRequest, Guid userId)
        {
            return new CreateOrderCommand(userId, orderRequest.AddressRequest == null ? null : new OrderAddress(orderRequest.AddressRequest.ZipCode, orderRequest.AddressRequest.Address, orderRequest.AddressRequest.City, orderRequest.AddressRequest.Country, orderRequest.AddressRequest.State), orderRequest.CardRequest == null ? null : new OrderCard(orderRequest.CardRequest.CardName, orderRequest.CardRequest.CardNumber, orderRequest.CardRequest.CVV, orderRequest.CardRequest.ExpiryDate));
        }
        public static OrderDto FromOrderToOrderDto(this Order order)
        {
            return new OrderDto(order.OrderId, order.Address == null ? null : order.Address.FromAddressToAddressDto(), order.PaymentMethod == null ? null : order.PaymentMethod.FromCardToCardDto());
        }
        public static OrderDtoWithItems FromOrderWithItemsToOrderDto(this Order order)
        {
            return new OrderDtoWithItems(order.OrderId, order.Address == null ? null : order.Address.FromAddressToAddressDto(), order.PaymentMethod == null ? null : order.PaymentMethod.FromCardToCardDto(), order.LineItems.FromLineItemsToLineItemDtoResponses());
        }
        public static AddressDto FromAddressToAddressDto(this Address address)
        {
            return new AddressDto(address.ZipCode, address.Street, address.City, address.Country, address.State);
        }
        public static CardDto FromCardToCardDto(this Card card)
        {
            return new CardDto(card.HoldersName, card.CardNumber, card.CVV, card.ExpiryDate);
        }
        public static LineItemDtoResponse FromLineItemToLineItemDtoResponse(this LineItem lineItem)
        {
            return new LineItemDtoResponse(lineItem.LineItemID, lineItem.ProductId, lineItem.ProductName, lineItem.Quantity, lineItem.Price, lineItem.PriceWithDiscount, lineItem.TotalItemPrice);
        }
        public static List<LineItemDtoResponse> FromLineItemsToLineItemDtoResponses(this List<LineItem> lineItems)
        {
            return lineItems.Select(x => x.FromLineItemToLineItemDtoResponse()).ToList();
        }
        public static OrderWrapperResponse FromOrderWrapperToOrderResponse(this OrderWrapperDto orderDto)
        {
            return new OrderWrapperResponse(orderDto.Order?.FromOrderWithItemsToOrderDto());
        }
        
    }
}
