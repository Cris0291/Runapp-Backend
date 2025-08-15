using MediatR;
using Microsoft.AspNetCore.Mvc;
using RunApp.Api.Routes;
using RunnApp.Application.Products.Queries.GetProduct;
using ErrorOr;
using RunApp.Domain.Products;
using RunApp.Api.Mappers.Products;
using RunnApp.Application.Products.Queries.GetProducts;
using Contracts.Products.Requests;
using RunnApp.Application.Products.Commands.CreateProduct;
using RunnApp.Application.Products.Commands.DeleteProduct;
using RunnApp.Application.Products.Commands.AddDiscount;
using RunnApp.Application.Products.Commands.RemoveDiscount;
using Microsoft.AspNetCore.Authorization;
using RunApp.Api.Services;
using RunApp.Api.CustomValidators;
using Contracts.Categories.Request;
using RunnApp.Application.Products.Commands.AddCategory;
using RunApp.Api.Mappers.Categories;
using RunnApp.Application.Products.Commands.DeleteCategory;
using RunnApp.Application.Products.Queries.GetProductsWithDiscount;
using RunnApp.Application.Products.Queries.ExistProduct;



namespace RunApp.Api.Controllers.Products
{

   
    public class ProductController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductController> _log;
        public ProductController(IMediator mediator, ILogger<ProductController> log)
        {
            _mediator = mediator;
            _log = log;
        }

        [Authorize]
        [HttpGet(ApiEndpoints.Products.GetProductById)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            Guid userId = HttpContext.GetUserId();
            var productQuery = new GetProductQuery(id, userId);

            var queryResponse = await _mediator.Send(productQuery);

            return queryResponse.MatchFirst(Ok, Problem);
        }

        [Authorize]
        [HttpGet(ApiEndpoints.Products.GetProducts)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllProductsRequestDto getAllProducts)
        {
            Guid userId = HttpContext.GetUserId();

            var options  = getAllProducts.SortBy.ConverToEnum();
            if (options.IsError) return Problem(options.Errors);

            GetProductsQuery getProductsQuery = new GetProductsQuery(userId, options.Value, getAllProducts.FilterByStars?.FromQueryStarValuesToRequestQuery(), getAllProducts.FilterByCategory?.FromQueryCategoryValuesToRequestQuey(), getAllProducts.FilterByPrice, getAllProducts.Search);

            var products = await _mediator.Send(getProductsQuery);
            return products.Match(value => Ok(value.AllProductsToProductsResponse()), Problem);
        }

        [Authorize]
        [HttpGet(ApiEndpoints.Products.GetProductsWithDiscount)]
        public async Task<IActionResult> GetProductsWithDiscount()
        {
            var result = await _mediator.Send(new GetProductsWithDiscountQuery());
            return Ok(result.ProductsWithImageAndDiscountToProductsResponse());
        }

        [Authorize]
        [HttpPost(ApiEndpoints.Products.Create)]
        public async Task<IActionResult> CreateProduct(CreateProductRequest createProduct)
        {
            Guid userId = HttpContext.GetUserId();
            CreateProductCommand productCommand = createProduct.ProductRequestToProductCommand(userId);
            ErrorOr<Product> productorError =  await _mediator.Send(productCommand);

            return productorError.Match(product => CreatedAtAction(nameof(Get), new { id = product.ProductId }, product.ProductToProductResponse()),
              error => Problem(error)); 
        }
       
        [Authorize]
        [HttpPut(ApiEndpoints.Products.UpdateProduct)]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, UpdateProductRequest updateProduct)
        {
            var productCommand = updateProduct.ProductRequestToProductCommand(id);

           var updatedProductResult =  await _mediator.Send(productCommand);

           return updatedProductResult.Match(result => Ok(result.ProductToProductResponse()), Problem);
        }

        [Authorize]
        [HttpDelete(ApiEndpoints.Products.DeleteProduct)]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {
            Guid userId = HttpContext.GetUserId();
            var deleteProductCommand = new DeleteProductCommand(id, userId);
            var deleteResult = await _mediator.Send(deleteProductCommand);

            return deleteResult.MatchFirst(result => Ok(), Problem);
        }

        [Authorize]
        [HttpPost(ApiEndpoints.Products.AddPriceWithDiscount)]
        public async Task<IActionResult> AddDiscount([FromRoute] Guid id, ProductDiscountRequest discount)
        {
            AddDiscountCommand discountCommand = new AddDiscountCommand(id, discount.NewPriceWithDiscount, discount.NewPromotionalText);

            var productWithDiscount = await _mediator.Send(discountCommand);

           return productWithDiscount.Match(result => Ok(), Problem);
        }

        [Authorize]
        [HttpDelete(ApiEndpoints.Products.DeletePriceWithDiscount)]
        public async Task<IActionResult> RemoveDiscount([FromRoute] Guid id)
        {
          var deletedDiscount = await _mediator.Send(new RemoveDiscountCommand(id));

            return deletedDiscount.MatchFirst(result =>Ok(), Problem);
        }

        [Authorize]
        [HttpPost(ApiEndpoints.Products.AddCategory)]
        public async Task<IActionResult> AddCategory([FromRoute] Guid id, CategoryRequest categoryRequest)
        {
            var result = await _mediator.Send(new AddCategoryCommand(id, categoryRequest.Category));
            return result.MatchFirst(value => Ok(value.CategoryToCategoryResponse()), Problem);
        }

        [Authorize]
        [HttpDelete(ApiEndpoints.Products.DeleteCategory)]
        public async Task<IActionResult> DeleteTag([FromRoute] DeleteCategoryRequest deleteTag)
        {
            var result = await _mediator.Send(new DeleteCategoryCommand(deleteTag.ProductId, deleteTag.CategoryId));
            return result.Match(value => Ok(), Problem);
        }

        [Authorize]
        [HttpGet(ApiEndpoints.Products.ExistProduct)]
        public async Task<IActionResult> ExistProduct([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new ExistProductQuery(id));

            return result.MatchFirst(value => Ok(), Problem);
        }
    }
}
