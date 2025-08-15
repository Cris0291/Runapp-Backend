using ErrorOr;
using MediatR;
using RunApp.Domain.Common.ValueType;

namespace RunnApp.Application.Orders.Commands.ModifyAddress
{
    public record ModifyAddressCommand(Guid OrderId,string ZipCode, string Street, string City, string Country, string State) : IRequest<ErrorOr<Address>>;
}
