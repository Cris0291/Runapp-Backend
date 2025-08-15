using ErrorOr;
using MediatR;
using RunApp.Domain.OrderAggregate;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.Orders.Queries.GetOrder
{
    public class GetOrderQueryHandler(IOrderRepository orderRepository, ICustomerProfileRepository customerProfile) : IRequestHandler<GetOrderQuery, ErrorOr<OrderWrapperDto>>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly ICustomerProfileRepository _customerProfile = customerProfile;
        public async Task<ErrorOr<OrderWrapperDto>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var user = await _customerProfile.GetCustomerProfile(request.UserId);
            if (user == null) throw new InvalidOperationException("User was not found with the given id");

            var result = await _orderRepository.GetCurrentOrder(user.Orders);

            return new OrderWrapperDto { Order = result };
        }
    }
}
