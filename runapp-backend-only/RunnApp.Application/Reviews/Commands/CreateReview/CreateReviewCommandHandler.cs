using MediatR;
using ErrorOr;
using RunnApp.Application.Common.Interfaces;
using RunApp.Domain.ReviewAggregate;
using RunApp.Domain.ReviewAggregate.ReviewErrors;

namespace RunnApp.Application.Reviews.Commands.CreateReview
{
    public class CreateReviewCommandHandler(IReviewsRepository reviewsRepository, IUnitOfWorkPattern unitOfWorkPattern, ICustomerProfileRepository customerProfileRepository) : IRequestHandler<CreateReviewCommand, ErrorOr<Review>>
    {
        private readonly IReviewsRepository _reviewsRepository = reviewsRepository;
        private readonly ICustomerProfileRepository _customerProfileRepository = customerProfileRepository;
        private readonly IUnitOfWorkPattern _unitOfWork = unitOfWorkPattern;
        public async Task<ErrorOr<Review>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerProfileRepository.GetCustomerProfile(request.UserId);
            if (customer == null) throw new InvalidOperationException("Something unexpected happened. User was not found");
            if (!customer.IsProductBought(request.ProductId)) return Error.Forbidden(code: "CanReviewedOnlyProductsThatWereBought", description: "Only bought products can be reviewed");

            bool existReview = await _reviewsRepository.ExistReview(request.UserId,request.ProductId);
            if (existReview) return ReviewError.UserCannotAddMoreThanOneReviewPerproduct;

            var review  = Review.CreateReview(request.Comment, request.Rating, request.ReviewDescriptionEnum, request.ProductId, request.UserId);

            await _reviewsRepository.AddReview(review);
            await _unitOfWork.CommitChangesAsync();

            return review;
        }
    }
}
