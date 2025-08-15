using FluentValidation;

namespace RunnApp.Application.CustomerProfiles.Commands.UpdateAddress
{
    public class UpdateAddressCommnadValidator : AbstractValidator<UpdateAddressCommand>
    {
        public UpdateAddressCommnadValidator()
        {
            RuleFor(x => x.ZipCode).Matches(@"^\d{5}(-\d{4})?$").WithMessage("Invalid ZIP Code format. It should be 5 digits or 5 digits + 4 digits (ZIP+4).");
            RuleFor(x => x.Street).NotEmpty().NotNull();
            RuleFor(x => x.City).NotEmpty().NotNull();
            RuleFor(x => x.Country).NotEmpty().NotNull();
            RuleFor(x => x.State).NotEmpty().NotNull();
        }
    }
}
