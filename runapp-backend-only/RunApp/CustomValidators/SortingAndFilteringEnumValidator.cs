using ErrorOr;
using RunnApp.Application.Common.SortingPagingFiltering;

namespace RunApp.Api.CustomValidators
{
    public static class SortingAndFilteringEnumValidator
    {
        public static ErrorOr<OrderByOptions> ConverToEnum(this string? sortingType)
        {
            if (sortingType == null) return OrderByOptions.SimpleOrder;

            if (!Enum.TryParse(sortingType, out OrderByOptions orderBy)) return Error.Validation(code: "SortingValueDidNotMatchSortingOptions", description: "Sorting value did not match sorting options");
            
            return orderBy;
        }
    }
}
