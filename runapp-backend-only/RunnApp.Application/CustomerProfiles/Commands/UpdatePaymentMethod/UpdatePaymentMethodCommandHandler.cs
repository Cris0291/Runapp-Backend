using ErrorOr;
using MediatR;
using RunApp.Domain.Common.ValueType;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.CustomerProfiles.Commands.UpdatePaymentMethod
{
    public class UpdatePaymentMethodCommandHandler(ICustomerProfileRepository customerProfileRepository, IUnitOfWorkPattern unitOfWorkPattern) : IRequestHandler<UpdatePaymentMethodCommand, ErrorOr<Card>>
    {
        private readonly ICustomerProfileRepository _customerProfileRepository = customerProfileRepository;
        private readonly IUnitOfWorkPattern _unitOfWorkPattern = unitOfWorkPattern;
        public async Task<ErrorOr<Card>> Handle(UpdatePaymentMethodCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerProfileRepository.GetCustomerProfile(request.UserId);
            if (customer == null) throw new InvalidOperationException("User profile was not found with given id");

            var card = customer.UpdatePaymentMethod(request.HoldersName, request.CardNumber, request.CVV, request.ExpiryDate);

            await _unitOfWorkPattern.CommitChangesAsync();
            return card;
        }
    }
}
