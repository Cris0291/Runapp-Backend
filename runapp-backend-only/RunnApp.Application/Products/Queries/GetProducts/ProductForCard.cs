using RunApp.Domain.ProductStatusAggregate;

namespace RunnApp.Application.Products.Queries.GetProducts
{
    public class ProductForCard()
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal ActualPrice { get; set; }
        public int? NumberOfReviews { get; set; }
        public int? NumberOflikes { get; set; }
        public double AverageRatings { get; set; }
        public decimal? PriceWithDiscount { get; set; }
        public string? PromotionalText { get; set; }
        public decimal? Discount { get; set; }
        public string? MainImage { get; set; }
        public bool? UserLike { get; set;}
        public IEnumerable<string> CategoryNames { get; set; }
    }
}
