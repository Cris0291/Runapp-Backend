namespace Contracts.Products.Responses
{
    public record LikesWithProductResponse(Guid? ProductId, string? ProductName, decimal? ProductPrice, Guid LikeId, bool? Like);
}
