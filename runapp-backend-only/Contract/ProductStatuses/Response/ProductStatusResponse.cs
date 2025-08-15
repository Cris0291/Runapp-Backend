namespace Contracts.ProductStatuses.Response
{
    public record ProductStatusResponse(bool? Like, bool? Dislike, bool? Viewed, bool? Bought);
}
