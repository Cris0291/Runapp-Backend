using ErrorOr;
using MediatR;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.LineItems.Commands.ChangeItemQuantity
{
    public class ChangeItemQuantityCommandHandler(IOrderRepository orderRepository, IUnitOfWorkPattern unitOfWorkPattern) : IRequestHandler<ChangeItemQuantityCommand, ErrorOr<Success>>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IUnitOfWorkPattern _unitOfWorkPattern = unitOfWorkPattern; 
        public async Task<ErrorOr<Success>> Handle(ChangeItemQuantityCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrder(request.OrderId);
            if (order == null) throw new InvalidOperationException("Order was not found in the database");

            bool isProductAdded = order.CheckProductExistence(request.ProductId);
            if (!isProductAdded) return Error.NotFound(code: "ItemWasNotFound", description: "Requested item was not found as a part of the order");

            var result = order.ChangeItemQuantity(request.Quantity, request.ProductId);
            if (result.IsError) return result.Errors;

            await _unitOfWorkPattern.CommitChangesAsync();

            return result.Value;
        }
    }
}
