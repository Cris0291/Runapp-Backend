using RunApp.Domain.CustomerProfileAggregate;

namespace RunnApp.Application.Common.Interfaces
{
    public interface ICustomerProfileRepository
    {
        Task CreateCustomerProfile(CustomerProfile customerProfile);
        Task<CustomerProfile?> GetCustomerProfile(Guid id);
        Task<bool> ExistCustomerProfile(Guid id);
    }
}
