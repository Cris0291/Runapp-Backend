using ErrorOr;
using MediatR;
using RunApp.Domain.StoreOwnerProfileAggregate.Stocks;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.Stocks.Commands.AddStock
{
    public class AddStockCommandHandler(IStoreOwnerProfileRepository profileRepository, IUnitOfWorkPattern unitOfWorkPattern) : IRequestHandler<AddStockCommand, ErrorOr<Stock>>
    {
        private readonly IStoreOwnerProfileRepository _profileRepository = profileRepository;
        private readonly IUnitOfWorkPattern _unitOfWorkPattern = unitOfWorkPattern;
        public async Task<ErrorOr<Stock>> Handle(AddStockCommand request, CancellationToken cancellationToken)
        {
            var storeOwner = await _profileRepository.GetStoreOwnerProfileWithStocksAndLogs(request.StoreProfileId, request.ProductId);
            if (storeOwner == null) throw new InvalidOperationException("Store profile was not found. An unexpected error occurred");

            var stockOrError = storeOwner.AddStock(request.AddedStock, request.ProductId);
            if (stockOrError.IsError) return stockOrError.Errors;

            await _unitOfWorkPattern.CommitChangesAsync();
            return stockOrError.Value;
        }
    }
}
