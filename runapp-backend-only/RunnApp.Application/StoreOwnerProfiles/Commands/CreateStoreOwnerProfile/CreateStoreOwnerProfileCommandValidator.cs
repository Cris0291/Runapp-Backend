using FluentValidation;
using RunApp.Domain.StoreOwnerProfileAggregate;

namespace RunnApp.Application.StoreOwnerProfiles.Commands.CreateStoreOwnerProfile
{
    public class CreateStoreOwnerProfileCommandValidator : AbstractValidator<CreateStoreOwnerProfileCommand>
    {
        public CreateStoreOwnerProfileCommandValidator()
        {
            RuleFor(x => x.StoreName).NotNull().NotEmpty();
            RuleFor(x => x.AddresCommand.ZipCode).GreaterThan(0);
            RuleFor(x => x.AddresCommand.Street).NotNull().NotEmpty();
            RuleFor(x => x.AddresCommand.City).NotNull().NotEmpty();
            RuleFor(x => x.AddresCommand.BuildingNumber).GreaterThan(0);
            RuleFor(x => x.AddresCommand.Country).NotNull().NotEmpty();
            RuleFor(x => x.CardCommand.HoldersName).NotNull().NotEmpty();
            RuleFor(x => x.CardCommand.CardNumber).GreaterThan(0);
            RuleFor(x => x.CardCommand.CVV).GreaterThan(0);
            RuleFor(x => x.CardCommand.ExpiryDate).NotEmpty();
            RuleFor(x => x.CardCommand.ExpiryDate).Must(y => y >= DateTime.UtcNow && y <= y.AddYears(5));
        }
    }
}
