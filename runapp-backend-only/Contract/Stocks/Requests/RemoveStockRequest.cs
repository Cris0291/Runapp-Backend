namespace Contracts.Stocks.Requests
{
    public record RemoveStockRequest(int RemovedQuantity, Guid ProductId);
   
}
