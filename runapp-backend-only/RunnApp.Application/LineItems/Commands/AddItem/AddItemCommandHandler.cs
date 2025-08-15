using ErrorOr;
using MediatR;
using RunApp.Domain.OrderAggregate.LineItems;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.LineItems.Commands.AddItem
{
    public class AddItemCommandHandler(IUnitOfWorkPattern unitOfWorkPattern, IOrderRepository orderRepository) : IRequestHandler<AddItemCommand, ErrorOr<LineItem>>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IUnitOfWorkPattern _unitOfWorkPattern = unitOfWorkPattern;
        public async Task<ErrorOr<LineItem>> Handle(AddItemCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrder(request.OrderId);
            if (order == null) throw new InvalidOperationException("Order was not found in the database");

            if (order.IsPaid) return Error.Validation(code: "CannotAddItemsToAnAlreadyPaidOrder", description: "Cannot add items to an already paid order");

            bool isProductAdded = order.CheckProductExistence(request.ProductId);
            if (isProductAdded) return Error.Failure(code: "ProductWasAlreadyAdded", description: "Product was already added");

            //if (Math.Ceiling(request.TotalPrice) != Math.Ceiling(request.Quantity * (request.PriceWithDiscount == null ? request.Price : request.PriceWithDiscount.Value))) return Error.Validation(code: "TotalQuantityDoesNotMathWithQuantityAndPrice", description: "Total item's quantity does not match with its quantity and price");

            var errorOrresult = order.AddItem(request.ProductId, request.ProductName, request.Quantity, request.Price, request.PriceWithDiscount, request.TotalPrice);
            if (errorOrresult.IsError) return errorOrresult.Errors;

            await _unitOfWorkPattern.CommitChangesAsync();

            return errorOrresult.Value;
        }
    }
}
