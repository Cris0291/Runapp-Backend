using MediatR;
using RunApp.Domain.ReviewAggregate.Events;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.Products.Events
{
    public class DeleteReviewEventHandler(IProductsRepository productsRepository, IUnitOfWorkPattern unitOfWorkPattern) : INotificationHandler<DeleteReviewEvent>
    {
        private readonly IProductsRepository _productsRepository = productsRepository;
        private readonly IUnitOfWorkPattern _unitOfWorkPattern = unitOfWorkPattern;
        public async  Task Handle(DeleteReviewEvent notification, CancellationToken cancellationToken)
        {
            var product = await _productsRepository.GetProduct(notification.ProductId);
            if (product == null) throw new InvalidOperationException("product was not found");

            product.DeleteReview(notification.Review);

            int wasDeleted = await _unitOfWorkPattern.CommitChangesAsync();
            if (wasDeleted == 0) throw new InvalidOperationException("Review could not be deleted");
        }
    }
}
