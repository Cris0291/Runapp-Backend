using RunApp.Domain.Common;

namespace RunApp.Domain.OrderAggregate.LineItems
{
    public class LineItem : Entity
    {
        internal LineItem() { }
        public Guid LineItemID { get; internal set; }
        public Guid ProductId { get; internal set; }
        public Guid OrderId { get; internal set; }
        public Order Order { get; internal set; }
        public int Quantity { get; internal set;}
        public decimal Price { get; internal set;}
        public decimal? PriceWithDiscount { get; internal set;}
        public decimal TotalItemPrice { get; internal set; }
        public string ProductName { get; internal set;}
    }
}
