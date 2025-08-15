using RunApp.Domain.StoreOwnerProfileAggregate;

namespace RunnApp.Application.Common.Interfaces
{
    public interface IStoreOwnerProfileRepository
    {
        Task CreateStoreOwnerProfile(StoreOwnerProfile storeOwnerProfile);
        Task<StoreOwnerProfile?> GetStoreOwnerProfile(Guid userId);
        Task<bool> ExistStoreOwnerProfile(Guid userId);
        Task<StoreOwnerProfile?> GetStoreOwnerProfileWithStocks(Guid userId);
        Task<StoreOwnerProfile?> GetStoreOwnerProfileWithStocksAndLogs(Guid storeOwnerProfileId);
        Task<StoreOwnerProfile?> GetStoreOwnerProfileWithStocksAndLogs(Guid storeOwnerProfileId, Guid productid);
    }
}
