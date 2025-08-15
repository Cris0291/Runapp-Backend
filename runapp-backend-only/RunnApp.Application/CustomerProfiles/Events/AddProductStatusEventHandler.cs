using MediatR;
using RunApp.Domain.ProductStatusAggregate.Events;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.CustomerProfiles.Events
{
    public class AddProductStatusEventHandler(ICustomerProfileRepository profileRepository, IUnitOfWorkPattern unitOfWorkPattern) : INotificationHandler<AddProductStatusEvent>
    {
        private readonly ICustomerProfileRepository _profileRepository = profileRepository;
        IUnitOfWorkPattern _unitOfWorkPattern = unitOfWorkPattern;
        public async Task Handle(AddProductStatusEvent notification, CancellationToken cancellationToken)
        {
            var customerProfile = await _profileRepository.GetCustomerProfile(notification.CustomerId);
            if (customerProfile == null) throw new InvalidOperationException("customer was not found with the given id");

            customerProfile.AddProductStatus(notification.ProductStatusId);

            await _unitOfWorkPattern.CommitChangesAsync();
        }
    }
}
