namespace RunnApp.Application.Products.Queries.GetProduct
{
    public record ReviewDto( string Comment, DateTime Date, string ReviewDescription, int Rating, Guid ReviewId, string UserName);
}
