using RunApp.Domain.Products;

namespace RunApp.Domain.StoreOwnerProfileAggregate.Sales
{
    public class Sale
    {
        internal Sale() { }
        public Guid SaleId { get; internal set; }
        public decimal AmountSold { get; internal set; }
        public int NumberOfitemsSold { get; internal set; }
        public DateTime DateOfTheSale { get; internal set; }
        public Product ProductSold { get; internal set; }
        public Guid ProductId { get; internal set; }
        public Guid StoreOwnerProfileId { get; internal set; }
}
}
