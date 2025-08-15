using ErrorOr;
using MediatR;

namespace RunnApp.Application.Stocks.Commands.RemoveStock
{
    public record RemoveStockCommand(int RemoveStock, Guid StoreOwnerId, Guid ProductId) : IRequest<ErrorOr<Success>>;
}
