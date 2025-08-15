using MediatR;
using RunApp.Domain.ProductAggregate.Events;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.CustomerProfiles.Events
{
    public class CreatedProductEventHandler(ICustomerProfileRepository customerProfileRepository, IUnitOfWorkPattern unitOfWorkPattern) : INotificationHandler<CreatedProductEvent>
    {
        private readonly ICustomerProfileRepository _customerProfileRepository = customerProfileRepository;
        private readonly IUnitOfWorkPattern _unitOfWorkPattern = unitOfWorkPattern;
        public async Task Handle(CreatedProductEvent notification, CancellationToken cancellationToken)
        {
            var user = await _customerProfileRepository.GetCustomerProfile(notification.UserId);
            if (user == null) throw new InvalidOperationException("User was not ffound with the given id");

            user.AddCreatedProduct(notification.Product.ProductId);

            await _unitOfWorkPattern.CommitChangesAsync();
        }
    }
}
