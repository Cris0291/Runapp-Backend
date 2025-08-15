using RunApp.Api.CustomValidators;

namespace Contracts.Reviews.Requests
{
    public record AddReviewRequest(string Comment, int Rating, [EnumValidator] ReviewDescriptions ReviewDescription);
}
