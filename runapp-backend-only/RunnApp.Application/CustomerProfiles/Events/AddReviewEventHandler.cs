using MediatR;
using RunApp.Domain.ReviewAggregate.Events;
using RunnApp.Application.Common.Interfaces;


namespace RunnApp.Application.CustomerProfiles.Events
{
    public class AddReviewEvetHandler(IUnitOfWorkPattern unitOfWorkPattern, ICustomerProfileRepository customerProfileRepository) : INotificationHandler<AddReviewEvent>
    {
        private readonly IUnitOfWorkPattern _unitOfWorkPattern = unitOfWorkPattern;
        private readonly ICustomerProfileRepository _customerProfileRepository = customerProfileRepository;
        public async Task Handle(AddReviewEvent notification, CancellationToken cancellationToken)
        {
            var customer = await _customerProfileRepository.GetCustomerProfile(notification.customerProfileId);
            if (customer == null) throw new InvalidOperationException("User was not found");

            customer.AddReview(notification.ProductId);

            await _unitOfWorkPattern.CommitChangesAsync();
        }
    }
}
