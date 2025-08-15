using ErrorOr;
using MediatR;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.Stocks.Commands.RemoveStock
{
    public class RemoveStockCommandHandler(IStoreOwnerProfileRepository storeOwnerProfile,IUnitOfWorkPattern unitOfWorkPattern) : IRequestHandler<RemoveStockCommand, ErrorOr<Success>>
    {
        private readonly IStoreOwnerProfileRepository _storeOwnerProfile = storeOwnerProfile;
        private readonly IUnitOfWorkPattern _unitOfWorkPattern = unitOfWorkPattern;
        public async Task<ErrorOr<Success>> Handle(RemoveStockCommand request, CancellationToken cancellationToken)
        {
            var profile = await _storeOwnerProfile.GetStoreOwnerProfileWithStocksAndLogs(request.StoreOwnerId, request.ProductId);
            if (profile == null) throw new InvalidOperationException("Store profile was not found. An unexpected error occurred");

            var errorOrResult =  profile.RemoveStock(request.RemoveStock, request.ProductId);
            if (errorOrResult.IsError) return errorOrResult.Errors;

            await _unitOfWorkPattern.CommitChangesAsync();
            return errorOrResult.Value;
        }
    }
}
