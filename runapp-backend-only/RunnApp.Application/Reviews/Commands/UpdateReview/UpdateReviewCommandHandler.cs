using ErrorOr;
using MediatR;
using RunApp.Domain.ReviewAggregate;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.Reviews.Commands.UpdateReview
{
    public class UpdateReviewCommandHandler(IReviewsRepository reviewsRepository, ICustomerProfileRepository customerProfileRepository, IUnitOfWorkPattern unitOfWorkPattern) : IRequestHandler<UpdateReviewCommand, ErrorOr<Review>>
    {
        private readonly IReviewsRepository _reviewsRepository = reviewsRepository;
        private readonly ICustomerProfileRepository _customerProfileRepository = customerProfileRepository;
        private readonly IUnitOfWorkPattern _unitOfWorkPattern = unitOfWorkPattern;
        public async Task<ErrorOr<Review>> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerProfileRepository.GetCustomerProfile(request.UserId);
            if (customer == null) throw new InvalidOperationException("Something unexpected happened. User was not found");
            if(!customer.IsProductBought(request.ProductId)) return Error.Forbidden(code: "CanReviewedOnlyProductsThatWereBought", description: "Only bought products can be reviewed");

            var review  = await _reviewsRepository.GetReview(request.UserId, request.ProductId);
            if (review == null) throw new InvalidOperationException("Review was not found");

            review.UpdateReview(request.ProductId, request.UserId ,request.Comment, request.Rating, request.ReviewDescriptionEnums);
            await _unitOfWorkPattern.CommitChangesAsync();

            return review;
        }
    }
}
