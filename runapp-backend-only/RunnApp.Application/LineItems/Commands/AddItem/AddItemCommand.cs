using ErrorOr;
using MediatR;
using RunApp.Domain.OrderAggregate.LineItems;

namespace RunnApp.Application.LineItems.Commands.AddItem
{
    public record AddItemCommand(Guid OrderId, Guid ProductId, string ProductName, int Quantity, decimal Price, decimal? PriceWithDiscount, decimal TotalPrice) : IRequest<ErrorOr<LineItem>>;
}
