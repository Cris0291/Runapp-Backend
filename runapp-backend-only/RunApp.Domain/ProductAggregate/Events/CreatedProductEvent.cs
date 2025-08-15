using RunApp.Domain.Common;
using RunApp.Domain.Products;

namespace RunApp.Domain.ProductAggregate.Events
{
    public record CreatedProductEvent(Product Product ,Guid UserId) : IDomainEvent;
}
