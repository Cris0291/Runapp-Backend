
namespace Contracts.Reviews.Responses
{
    public record ReviewResponse(string Comment, int Rating, DateTime Date, string ReviewDescription, Guid Id);
}
