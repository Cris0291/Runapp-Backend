using MediatR;
using RunApp.Domain.CustomerProfileAggregate;
using RunApp.Domain.UserAggregate.Events;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.CustomerProfiles.Events
{
    public class CreateCustomerProfileEventHandler(ICustomerProfileRepository customerProfileRepository, IUnitOfWorkPattern unitOfWork) : INotificationHandler<CreateCustomerProfileEvent>
    {
        private readonly ICustomerProfileRepository _customerProfileRepository = customerProfileRepository;
        private readonly IUnitOfWorkPattern _unitOfWork = unitOfWork;
        public async Task Handle(CreateCustomerProfileEvent notification, CancellationToken cancellationToken)
        {
            var customerProfile = CustomerProfile.CreateCustomerProfile(notification.UserName, notification.Email, notification.NickName, notification.UserId);
            if (customerProfile.IsError) throw new InvalidOperationException("User could not be created due to incorrect data");

            await _customerProfileRepository.CreateCustomerProfile(customerProfile.Value);

            int rowsAdded =  await _unitOfWork.CommitChangesAsync();
            if(rowsAdded == 0) throw new InvalidOperationException("User could not be created due to an unexpected error");
        }
    }
}
