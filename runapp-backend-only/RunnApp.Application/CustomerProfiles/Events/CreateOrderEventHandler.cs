using MediatR;
using RunApp.Domain.OrderAggregate.Events;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.CustomerProfiles.Events
{
    public class CreateOrderEventHandler(ICustomerProfileRepository profileRepository, IUnitOfWorkPattern unitOfWorkPattern) : INotificationHandler<CreateOrderEvent>
    {
        private readonly ICustomerProfileRepository _profileRepository = profileRepository;
        private readonly IUnitOfWorkPattern _unitOfWorkPattern = unitOfWorkPattern;
        public async Task Handle(CreateOrderEvent notification, CancellationToken cancellationToken)
        {
            var user = await _profileRepository.GetCustomerProfile(notification.UserId);
            if (user == null) throw new InvalidOperationException("User was not found with the given id");

            user.AddOrder(notification.OrderId);

            await _unitOfWorkPattern.CommitChangesAsync();
        }
    }
}
