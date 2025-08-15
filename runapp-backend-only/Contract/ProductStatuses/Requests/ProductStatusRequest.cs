namespace Contracts.ProductStatuses.Requests
{
   public record ProductStatusRequest(bool? Like, bool? Dislike, bool? Viewed, bool? Bought);
}
