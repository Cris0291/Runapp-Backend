using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TestsUtilities")]
namespace RunApp.Domain.ProductAggregate.ValueTypes
{
    public class PriceOffer
    {
        internal PriceOffer() { }
        public decimal? PriceWithDiscount { get; internal set; }
        public string? PromotionalText { get; internal set; }
        public decimal? Discount { get; internal set; }
    }
}
