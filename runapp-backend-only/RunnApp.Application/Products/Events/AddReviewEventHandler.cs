using MediatR;
using RunApp.Domain.ReviewAggregate.Events;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.Products.Events
{
    public class AddReviewEventHandler(IUnitOfWorkPattern unitOfWorkPattern, IProductsRepository productsRepository) : INotificationHandler<AddReviewEvent>
    {
        private readonly IUnitOfWorkPattern _unitOfWorkPattern = unitOfWorkPattern;
        private readonly IProductsRepository _productsRepository = productsRepository;
        public async Task Handle(AddReviewEvent notification, CancellationToken cancellationToken)
        {
            var product = await _productsRepository.GetProduct(notification.ProductId);
            if (product == null) throw new InvalidOperationException("Product was not found");

            product.AddReview(notification.Review);
            await _unitOfWorkPattern.CommitChangesAsync();
        }
    }
}
