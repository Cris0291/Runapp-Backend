using ErrorOr;
using MediatR;
using RunApp.Domain.Common.ValueType;

namespace RunnApp.Application.CustomerProfiles.Commands.AddAddress
{
    public record AddAddressCommand(Guid UserId, string ZipCode, string Street, string City, string Country, string State) : IRequest<ErrorOr<Address>>;
}
