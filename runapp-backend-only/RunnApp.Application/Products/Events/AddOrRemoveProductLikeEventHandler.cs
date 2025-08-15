using MediatR;
using RunApp.Domain.ProductStatusAggregate.Events;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.Products.Events
{
    public class AddOrRemoveProductLikeEventHandler(IProductsRepository productsRepository, IUnitOfWorkPattern unitOfWorkPattern) : INotificationHandler<AddOrRemoveProductLike>
    {
        private readonly IProductsRepository _productsRepository = productsRepository;
        private readonly IUnitOfWorkPattern _unitOfWorkPattern = unitOfWorkPattern;
        public async Task Handle(AddOrRemoveProductLike notification, CancellationToken cancellationToken)
        {
            var product = await _productsRepository.GetProduct(notification.ProductId);
            if (product == null) throw new InvalidOperationException("Liked product was not found");

            product.AddProductLike(notification.added);

            await _unitOfWorkPattern.CommitChangesAsync();
        }
    }
}
