using MediatR;
using RunApp.Domain.ProductAggregate.Events;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.CustomerProfiles.Events
{
    public class DeleteProductEventHandler(IUnitOfWorkPattern unitOfWorkPattern, ICustomerProfileRepository customerProfile) : INotificationHandler<DeleteProductEvent>
    {
        private readonly IUnitOfWorkPattern _unitOfWorkPattern = unitOfWorkPattern;
        private readonly ICustomerProfileRepository _customerProfile = customerProfile;
        public async Task Handle(DeleteProductEvent notification, CancellationToken cancellationToken)
        {
            var user = await _customerProfile.GetCustomerProfile(notification.UserId);
            if (user == null) throw new InvalidOperationException("User was not found with the given id");

            var t1 = user.BoughtProducts.Remove(notification.ProductId);
            var t2 = user.CreatedProducts.Remove(notification.ProductId);
            var t3 = user.Reviews.Remove(notification.ProductId);

            await _unitOfWorkPattern.CommitChangesAsync();
        }
    }
}
