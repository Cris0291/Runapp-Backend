using MediatR;
using RunApp.Domain.OrderAggregate.Events;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.CustomerProfiles.Events
{
    public class AddBoughtProductsEventHandler(ICustomerProfileRepository profileRepository, IUnitOfWorkPattern unitOfWorkPattern) : INotificationHandler<AddBoughtProductsEvent>
    {
        private readonly ICustomerProfileRepository _profileRepository = profileRepository;
        private readonly IUnitOfWorkPattern _unitOfWorkPattern = unitOfWorkPattern;
        public async Task Handle(AddBoughtProductsEvent notification, CancellationToken cancellationToken)
        {
            var user = await _profileRepository.GetCustomerProfile(notification.UserId);
            if (user == null) throw new InvalidOperationException("User profile was not found with given id");

            user.AddBoughtProducts(notification.BoughtProducts);

            await _unitOfWorkPattern.CommitChangesAsync();
        }
    }
}
