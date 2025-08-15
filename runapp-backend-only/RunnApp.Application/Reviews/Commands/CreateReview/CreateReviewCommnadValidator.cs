using FluentValidation;

namespace RunnApp.Application.Reviews.Commands.CreateReview
{
    public class CreateReviewCommnadValidator : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewCommnadValidator()
        {
            RuleFor(command => command.Comment).NotNull().NotEmpty();
            RuleFor(command => command.Rating).GreaterThan(0);
        }
    }
}
