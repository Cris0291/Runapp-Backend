using RunApp.Domain.Common;
using RunApp.Domain.Products;

namespace RunApp.Domain.ProductAggregate.Events
{
    public record CreateStockEvent(Product Product, Guid StoreOwnerId) : IDomainEvent;
   
}
