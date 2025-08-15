using MediatR;
using RunApp.Domain.ReviewAggregate.Events;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.Products.Events
{
    public class UpdateReviewEventHandler(IProductsRepository productsRepository, IUnitOfWorkPattern unitOfWorkPattern) : INotificationHandler<UpdateReviewEvent>
    {
        private readonly IProductsRepository _productsRepository = productsRepository;
        private readonly IUnitOfWorkPattern _unitOfWorkPattern = unitOfWorkPattern;

        public async Task Handle(UpdateReviewEvent notification, CancellationToken cancellationToken)
        {
            var product = await _productsRepository.GetProduct(notification.ProductId);
            if (product == null) throw new InvalidOperationException("Product was not found");

            product.UpdateReview(notification.Review, notification.OldRating);
            await _unitOfWorkPattern.CommitChangesAsync();
        }
    }
}
