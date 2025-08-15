using FluentValidation;

namespace RunnApp.Application.CustomerProfiles.Commands.AddAddress
{
    public class AddAddressCommandValidator : AbstractValidator<AddAddressCommand>
    {
        public AddAddressCommandValidator()
        {
            RuleFor(x => x.ZipCode).NotEmpty().Matches(@"^\d{5}(-\d{4})?$")
            .WithMessage("Invalid ZIP Code format. It should be 5 digits or 5 digits + 4 digits (ZIP+4).");
            RuleFor(x => x.Street).NotEmpty().NotNull();
            RuleFor(x => x.City).NotEmpty().NotNull();
            RuleFor(x => x.Country).NotEmpty().NotNull();
            RuleFor(x => x.State).NotEmpty().NotNull();
        }
    }
}
