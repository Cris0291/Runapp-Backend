using MediatR;
using RunApp.Domain.ReviewAggregate.Events;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.CustomerProfiles.Events
{
    public class DeleteReviewEventHandler(ICustomerProfileRepository customerProfileRepository, IUnitOfWorkPattern unitOfWorkPattern) : INotificationHandler<DeleteReviewEvent>
    {
        private readonly ICustomerProfileRepository _customerProfileRepository = customerProfileRepository;
        private readonly IUnitOfWorkPattern _unitOfWorkPattern = unitOfWorkPattern;
        public async Task Handle(DeleteReviewEvent notification, CancellationToken cancellationToken)
        {
            var customer  = await _customerProfileRepository.GetCustomerProfile(notification.CustomerProfileId);
            if(customer == null) throw new InvalidOperationException("Customer was not found");

            customer.DeleteReview(notification.ProductId);

            int wasDeleted = await _unitOfWorkPattern.CommitChangesAsync();
            if (wasDeleted == 0) throw new InvalidOperationException("Review could not be deleted");
        }
    }
}
