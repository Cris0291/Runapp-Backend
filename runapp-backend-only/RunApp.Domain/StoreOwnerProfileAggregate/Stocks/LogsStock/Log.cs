namespace RunApp.Domain.StoreOwnerProfileAggregate.Stocks.LogsStock
{
    public class Log
    {
        internal Log() { }
        public Guid LogId { get; internal set; }
        public DateTime? StockAddedDate { get; internal set; }
        public DateTime? StockRemoveDate { get; internal set; }
        public DateTime? StockSoldDate { get; internal set; }
        public DateTime StockDate { get; internal set; }
        public int? AddedStock { get; internal set; }
        public int? SoldStock { get; internal set; }
        public int? RemovedStock { get; internal set; }
        public Guid StockId { get; internal set; }
    }
}
