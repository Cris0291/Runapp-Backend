using FluentValidation;

namespace RunnApp.Application.Reviews.Commands.UpdateReview
{
    public class UpdateReviewCommandValidator : AbstractValidator<UpdateReviewCommand>
    {
        public UpdateReviewCommandValidator()
        {
            RuleFor(x => x.Comment).NotEmpty().NotNull();
            RuleFor(x => x.Rating).GreaterThan(0);
        }
    }
}
