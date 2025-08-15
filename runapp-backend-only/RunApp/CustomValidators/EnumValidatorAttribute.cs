using Contracts.Reviews.Requests;
using System.ComponentModel.DataAnnotations;
using RunApp.Domain.ReviewAggregate.ReviewEnum;

namespace RunApp.Api.CustomValidators
{
    public class EnumValidatorAttribute: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is not null)
            {
                string descriptionEnum = ((ReviewDescriptions)value).ToString();
                if (Enum.TryParse(descriptionEnum,  out ReviewDescriptions review))
                {
                    if (ReviewDescriptionEnums.TryFromName(review.ToString(), out ReviewDescriptionEnums reviewEnum)) return ValidationResult.Success;
                    return new ValidationResult("Description is not valid", [validationContext.MemberName]);
                }

                return new ValidationResult("Description is not valid", [validationContext.MemberName]);
            }

            return new ValidationResult("review description must not be empty", [validationContext.MemberName]);
        }
    }
}
