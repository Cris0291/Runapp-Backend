using RunApp.Domain.CustomerProfileAggregate;
using RunApp.Domain.ReviewAggregate;

namespace RunnApp.Application.Products.Queries.GetProduct
{
    public class ReviewAndCustomerDto
    {
        public Review Review { get; set; }
        public CustomerProfile? CustomerProfile { get; set; }
    }
}
