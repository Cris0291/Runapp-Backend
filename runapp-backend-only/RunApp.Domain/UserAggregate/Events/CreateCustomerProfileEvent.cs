using RunApp.Domain.Common;

namespace RunApp.Domain.UserAggregate.Events
{
    public record CreateCustomerProfileEvent(Guid UserId, string Email, string UserName, string NickName) : IDomainEvent;
}
