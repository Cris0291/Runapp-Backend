using MediatR;
using RunnApp.Application.Common.Interfaces;
using RunnApp.Application.Products.Queries.GetProducts;

namespace RunnApp.Application.Products.Queries.GetProductsWithDiscount
{
    public class GetProductWithDiscountQueryHandler(IProductsRepository productsRepository, ILeftJoinRepository leftJoinRepository) : IRequestHandler<GetProductsWithDiscountQuery, IEnumerable<ProductWithMainImage>>
    {
        IProductsRepository _productsRepository = productsRepository;
        ILeftJoinRepository _leftJoinRepository = leftJoinRepository;
        public async Task<IEnumerable<ProductWithMainImage>> Handle(GetProductsWithDiscountQuery request, CancellationToken cancellationToken)
        {
            var productsWithDiscount =  _productsRepository.GetLatestDiscounts();
            var productsWithImageAndDiscount = _leftJoinRepository.GetProductsWithImage(productsWithDiscount);
            var products = await _leftJoinRepository.ExecuteQuery(productsWithImageAndDiscount);
            return products;
        }
    }
}
