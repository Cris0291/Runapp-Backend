using FluentValidation;

namespace RunnApp.Application.CustomerProfiles.Commands.UpdateAccountInfo
{
    public class UpdateAccountInfoCommandValidator : AbstractValidator<UpdateAccountInfoCommand>
    {
        public UpdateAccountInfoCommandValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.NickName).NotNull().NotEmpty();
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.")
            .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
            .WithMessage("Invalid email format.");
        }
    }
}
