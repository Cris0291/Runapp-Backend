using ErrorOr;
using MediatR;
using RunApp.Domain.Products;

namespace RunnApp.Application.Products.Commands.UpdateProduct
{
    public record UpdateProductCommand(string Name, string Description, decimal Price, ICollection<string> Bulletpoints, 
        string Brand, string Type, string Color, double Weight, Guid ProductId) : IRequest<ErrorOr<Product>>;
}
