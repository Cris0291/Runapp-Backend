using ErrorOr;
using MediatR;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.Orders.Commands.PayOrder
{
    public class PayOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWorkPattern unitOfWorkPattern) : IRequestHandler<PayOrderCommand, ErrorOr<Success>>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IUnitOfWorkPattern _unitOfWorkPattern = unitOfWorkPattern;
        public async Task<ErrorOr<Success>> Handle(PayOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrder(request.OrderId);
            if (order == null) throw new InvalidOperationException("Order was not found in the database");

            var result = order.PayOrder(request.UserId);

            if (result.IsError) return result.Errors;

            await _unitOfWorkPattern.CommitChangesAsync();

            return result.Value;
        }
    }
}
