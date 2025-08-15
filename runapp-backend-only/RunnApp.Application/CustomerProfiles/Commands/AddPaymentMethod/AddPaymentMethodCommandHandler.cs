using ErrorOr;
using MediatR;
using RunApp.Domain.Common.ValueType;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.CustomerProfiles.Commands.AddPaymentMethod
{
    public class AddPaymentMethodCommandHandler(ICustomerProfileRepository profileRepository, IUnitOfWorkPattern unitOfWorkPattern) : IRequestHandler<AddPaymentMethodCommand, ErrorOr<Card>>
    {
        private readonly ICustomerProfileRepository _profileRepository = profileRepository;
        private readonly IUnitOfWorkPattern _unitOfWorkPattern = unitOfWorkPattern;
        public async Task<ErrorOr<Card>> Handle(AddPaymentMethodCommand request, CancellationToken cancellationToken)
        {
            var user = await _profileRepository.GetCustomerProfile(request.UserId);
            if (user == null) throw new InvalidOperationException("User profile was not found with given id");

            var card = user.AddPaymentMethod(request.HoldersName, request.CardNumber, request.CVV, request.ExpiryDate);

            await _unitOfWorkPattern.CommitChangesAsync();
            return card;
        }
    }
}
