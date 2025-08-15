using ErrorOr;

namespace RunApp.Domain.StoreOwnerProfileAggregate.StoreOwnerProfileErrors
{
    public static class StoreOwnerprofileError
    {
        public static Error InitialInvestmentCannotBeLowerThan5000 = Error.Validation(code: "InitialInvestmentCannotBeLowerThan5000", description: "Initial investment cannot be lower than 5000 dollars");
    }
}
