namespace RunnApp.Application.Products.Queries.GetProduct
{
    public class ProductItemDto()
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal ActualPrice { get; set; }
        public string Description { get;  set; }
        public int? NumberOfReviews { get; set; }
        public int? NumberOflikes { get; set; }
        public double AverageRatings { get; set; }
        public decimal? PriceWithDiscount { get; set; }
        public string? PromotionalText { get; set; }
        public decimal? Discount { get; set; }
        public string? MainImage { get; set; }
        public bool Like { get; set; }
        public bool IsBought { get; set; }
        public bool IsBoughtWithReview { get; set; }
        public IEnumerable<string> CategoryNames { get; set; }
        public IEnumerable<string> BulletPoints { get; set; }
        public IEnumerable<ReviewDto>? Reviews { get; set; }
    }
}
