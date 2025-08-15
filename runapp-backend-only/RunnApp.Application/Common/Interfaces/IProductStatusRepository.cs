using RunApp.Domain.Products;
using RunApp.Domain.ProductStatusAggregate;

namespace RunnApp.Application.Common.Interfaces
{
    public interface IProductStatusRepository
    {
        Task AddProductStatus(ProductStatus productStatus);
        Task<ProductStatus?> GetProductStatus(Guid productId, Guid customerId);
        Task<bool> ExistProductStatus(Guid productId, Guid customerId);
        Task<IEnumerable<ProductStatus>> GetProductStatuses(Guid customerId);


    }
}
