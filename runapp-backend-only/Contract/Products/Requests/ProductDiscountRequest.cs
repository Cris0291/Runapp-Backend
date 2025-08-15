namespace Contracts.Products.Requests
{
    public record ProductDiscountRequest(decimal NewPriceWithDiscount, string NewPromotionalText);
}
