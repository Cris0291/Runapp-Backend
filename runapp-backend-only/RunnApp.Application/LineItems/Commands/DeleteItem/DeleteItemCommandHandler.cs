using ErrorOr;
using MediatR;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.LineItems.Commands.DeleteItem
{
    public class DeleteItemCommandHandler(IOrderRepository orderRepository, IUnitOfWorkPattern unitOfWorkPattern) : IRequestHandler<DeleteItemCommand, ErrorOr<Success>>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IUnitOfWorkPattern _unitOfWorkPattern = unitOfWorkPattern; 
        public async Task<ErrorOr<Success>> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrder(request.OrderId);
            if (order == null) throw new InvalidOperationException("Order was not found in the database");

            bool isProductAdded = order.CheckProductExistence(request.ProductId);
            if (!isProductAdded) return Error.NotFound(code: "ItemWasNotFound", description: "Requested item could not be deleted since it was not found as a part of the order");

            var item = order.DeleteItem(request.ProductId);
            await _orderRepository.DeleteItem(item);

            await _unitOfWorkPattern.CommitChangesAsync();

            return Result.Success;
        }
    }
}
