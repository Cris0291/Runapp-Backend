using ErrorOr;

namespace RunApp.Domain.CustomerProfileAggregate.CustomerProfileErrors
{
    public static class CustomerProfileError
    {
        public static Error UserNameCannotBeNullOrEmpty = Error.Validation(code: "UserNameCannotBeNullOrEmpty", description: "Username must not be empty");
        public static Error NickNameCannotBeNullOrEmpty = Error.Validation(code: "NickNameCannotBeNullOrEmpty", description: "Nickname must not be empty");
        public static Error EmailCannotBeNullOrEmpty = Error.Validation(code: "EmailCannotBeNullOrEmpty", description: "Email must not be empty");
        public static Error EmailDoesNotHaveCorrectFormat = Error.Validation(code: "EmailDoesNotHaveCorrectFormat", description: "Email does not have correct format");
    }
}
