using ErrorOr;
using MediatR;
using RunApp.Domain.OrderAggregate;
using RunnApp.Application.Common.Interfaces;
using RunApp.Domain.Common.ValueType;

namespace RunnApp.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler(IUnitOfWorkPattern unitOfWorkPattern, IOrderRepository orderRepository, ICustomerProfileRepository customerProfile) : IRequestHandler<CreateOrderCommand, ErrorOr<Order>>
    {
        private readonly IUnitOfWorkPattern _unitOfWorkPattern = unitOfWorkPattern;
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly ICustomerProfileRepository _customerProfile = customerProfile;
        public async Task<ErrorOr<Order>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            IEnumerable<Order>? userOrders = null;
            var user = await _customerProfile.GetCustomerProfile(request.UserId);
            if (user == null) throw new InvalidOperationException("User was not found with the given id");

            if(user.Orders.Count > 0)
            {
                userOrders = await _orderRepository.GetUserOrders(user.Orders);
            }
            

            var address = request.OrderAddress == null ? null : new Address
            {
                Country = request.OrderAddress.Country,
                City = request.OrderAddress.City,
                ZipCode = request.OrderAddress.ZipCode,
                State = request.OrderAddress.State,
                Street = request.OrderAddress.Address
            };

            var card = request.OrderCard == null ? null : new Card
            {
                CardNumber = request.OrderCard.CardNumber,
                HoldersName = request.OrderCard.CardName,
                CVV = request.OrderCard.CVV,
                ExpiryDate = request.OrderCard.ExpiryDate,
            };

            if (userOrders != null)
            {
                if (userOrders.Where(x => !x.IsPaid).Count() > 1)
                {
                    throw new InvalidOperationException("Cannot have two active orders at the same time");
                }
                else if (userOrders.Where(x => !x.IsPaid).Count() == 1)
                {
                    return Error.Validation(code: "CannotCreateAnewOrderWhileThereIsAnActiveOne", description: "Cannot create a new order while there is an active order");
                }
            }

            var order = Order.CreateOrder(request.UserId, address , card);

            await _orderRepository.CreateOrder(order);
            order.CommunicateToUserOrderCreation();

            int rowsChanged = await _unitOfWorkPattern.CommitChangesAsync();
            if (rowsChanged == 0) throw new InvalidOperationException("Order could not be added to the database");

            return order;
        }
    }
}
