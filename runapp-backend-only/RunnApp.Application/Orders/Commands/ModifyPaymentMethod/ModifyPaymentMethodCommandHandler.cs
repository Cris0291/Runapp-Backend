using ErrorOr;
using MediatR;
using RunApp.Domain.Common.ValueType;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.Orders.Commands.ModifyPaymentMethod
{
    public class ModifyPaymentMethodCommandHandler(IOrderRepository orderRepository, IUnitOfWorkPattern unitOfWorkPattern) : IRequestHandler<ModifyPaymentMethodCommand, ErrorOr<Card>>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IUnitOfWorkPattern _unitOfWorkPattern = unitOfWorkPattern;
        public async Task<ErrorOr<Card>> Handle(ModifyPaymentMethodCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderWithoutItems(request.OrderId);
            if (order == null) throw new InvalidOperationException("Order was not found in the database");

            order.ModifyPaymentMethod(request.HoldersName, request.CardNumber, request.CVV, request.ExpiryDate);

            await _unitOfWorkPattern.CommitChangesAsync();
            return order.PaymentMethod!;
        }
    }
}
