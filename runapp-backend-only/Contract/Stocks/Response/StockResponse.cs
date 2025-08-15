namespace Contracts.Stocks.Response
{
    public record StockResponse(int AddedStock, int SoldStock, string ProductName, string Brand, string ProductType, Guid StockId);
}
