using FluentValidation;

namespace RunnApp.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.OrderAddress!.ZipCode).NotEmpty().Matches(@"^\d{5}(-\d{4})?$")
            .WithMessage("Invalid ZIP Code format. It should be 5 digits or 5 digits + 4 digits (ZIP+4).").When(x => x.OrderAddress != null);
            RuleFor(x => x.OrderAddress!.Address).NotEmpty().NotNull().When(x => x.OrderAddress != null);
            RuleFor(x => x.OrderAddress!.City).NotEmpty().NotNull().When(x => x.OrderAddress != null); 
            RuleFor(x => x.OrderAddress!.Country).NotEmpty().NotNull().When(x => x.OrderAddress != null);
            RuleFor(x => x.OrderCard!.CardName).NotEmpty().NotNull().When(x => x.OrderCard != null);
            RuleFor(x => x.OrderCard!.CardNumber).Matches(@"^\d{13,19}$").WithMessage("Card number must be between 13 and 19 digits.").When(x => x.OrderCard != null);
            RuleFor(x => x.OrderCard!.CVV).Matches(@"^\d{3}$").WithMessage("Invalid CVV format for the given card type.").When(x => x.OrderCard != null);
            RuleFor(x => x.OrderCard!.ExpiryDate).Matches(@"^(0[1-9]|1[0-2])\/\d{2}(\d{2})?$").WithMessage("Expiration date must be in MM/YY or MM/YYYY format.").When(x => x.OrderCard != null)
            .Must(BeAValidExpirationDate).WithMessage("Expiration date cannot be in the past.").When(x => x.OrderCard != null);
        }

        private bool BeAValidExpirationDate(string expirationDate)
        {
            // Parse the expiration date
            DateTime expiryDate;
            if (expirationDate.Length == 5) // MM/YY format
            {
                // Expiry year (YY) needs to be 20 + year to form a valid year
                string year = "20" + expirationDate.Substring(3, 2);
                string month = expirationDate.Substring(0, 2);
                string fullDate = month + "/01/" + year;
                return DateTime.TryParse(fullDate, out expiryDate) && expiryDate >= DateTime.Now;
            }
            else if (expirationDate.Length == 7) // MM/YYYY format
            {
                return DateTime.TryParseExact(expirationDate, "MM/yyyy", null, System.Globalization.DateTimeStyles.None, out expiryDate) && expiryDate >= DateTime.Now;
            }
            return false;
        }
    }
}
