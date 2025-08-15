using ErrorOr;
using MediatR;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.Reviews.Commands.DeleteReview
{
    public class DeleteReviewCommandHandler(IReviewsRepository reviewsRepository, IUnitOfWorkPattern unitOfWorkPattern) : IRequestHandler<DeleteReviewCommand, ErrorOr<Success>>
    {
        private readonly IReviewsRepository _reviewsRepository = reviewsRepository;
        private readonly IUnitOfWorkPattern _unitOfWorkPattern = unitOfWorkPattern;
        public async Task<ErrorOr<Success>> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _reviewsRepository.GetReview(request.UserId, request.ProductId);
            if (review == null) return Error.NotFound(code: "ReviewWasNotFoundWithGivenId", description: "Requested review was not found");

            review.RemoveReview(review.ReviewId, request.ProductId, request.UserId);

            await _reviewsRepository.RemoveReview(review);

            int wasDeleted = await _unitOfWorkPattern.CommitChangesAsync();
            if (wasDeleted == 0) throw new InvalidOperationException("Review could not be deleted");

            return Result.Success;
        }
    }
}
