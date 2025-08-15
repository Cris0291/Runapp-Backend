using Contracts.Stocks.Requests;
using Contracts.Stocks.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RunApp.Api.Routes;
using RunApp.Api.Services;
using RunnApp.Application.Stocks.Commands.AddStock;
using RunnApp.Application.Stocks.Commands.RemoveStock;

namespace RunApp.Api.Controllers.StockController
{
   
    public class StockController(ISender mediator) : ApiController
    {
        private readonly ISender _mediator = mediator;

        [Authorize("StoreProfile")]
        [HttpPost(ApiEndpoints.StoreOwnerProfiles.AddStock)]
        public async Task<IActionResult> AddStock(AddStockRequest stockRequest)
        {
           Guid storeOwnerId =  HttpContext.GetStoreOwnerId();
           var result = await _mediator.Send(new AddStockCommand(stockRequest.AddedStock,stockRequest.ProductId, storeOwnerId));

            return result.Match(value => Ok(new StockResponse(value.TotalQuantity, value.SoldQuantity,value.ProductName, value.Brand, value.ProductType, value.StockId)), Problem);
        }

        [HttpPost(ApiEndpoints.StoreOwnerProfiles.RemoveStock)]
        public async Task<IActionResult> RemoveStock(RemoveStockRequest removeStockRequest)
        {
            Guid storeOwnerId = HttpContext.GetStoreOwnerId();
            var result = await _mediator.Send(new RemoveStockCommand(removeStockRequest.RemovedQuantity, storeOwnerId, removeStockRequest.ProductId));

            return result.Match(value => Ok(), Problem);
        }
    }
}
