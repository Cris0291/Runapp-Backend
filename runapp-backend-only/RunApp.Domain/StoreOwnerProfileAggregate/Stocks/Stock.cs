using RunApp.Domain.StoreOwnerProfileAggregate.Stocks.LogsStock;

namespace RunApp.Domain.StoreOwnerProfileAggregate.Stocks
{
    public class Stock
    {
        internal Stock() { }
        public Guid StockId { get; internal set; }
        public int TotalQuantity { get; internal set; }
        public int SoldQuantity { get; internal set; }
        public int RemovedQuantity { get; internal set; }
        public string ProductName { get; internal set; }
        public string Brand { get; internal set; }
        public string  ProductType { get; internal set; }
        public Guid StockProductId { get; internal set; }
        public Guid StoreOwnerProfileId { get; internal set; }
        public List<Log> Logs { get; internal set; }
    }
}
