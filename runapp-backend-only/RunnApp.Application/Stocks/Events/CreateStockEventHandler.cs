using MediatR;
using RunApp.Domain.ProductAggregate.Events;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.Stocks.Events
{
    public class CreateStockEventHandler(IStoreOwnerProfileRepository profileRepository, IUnitOfWorkPattern unitOfWorkPattern) : INotificationHandler<CreateStockEvent>
    {
        private IStoreOwnerProfileRepository _profileRepository = profileRepository;
        private IUnitOfWorkPattern _unitOfWorkPattern = unitOfWorkPattern;
        public async Task Handle(CreateStockEvent notification, CancellationToken cancellationToken)
        {
            var storeProfile = await _profileRepository.GetStoreOwnerProfileWithStocks(notification.StoreOwnerId);
            if (storeProfile == null) throw new InvalidOperationException("Current user was not found");

            storeProfile.CreateStock(notification.Product);

            await _unitOfWorkPattern.CommitChangesAsync();
        }
    }
}
