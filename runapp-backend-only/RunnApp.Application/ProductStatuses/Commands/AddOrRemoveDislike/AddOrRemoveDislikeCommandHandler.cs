using ErrorOr;
using MediatR;
using RunApp.Domain.ProductStatusAggregate;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.ProductStatuses.Commands.AddOrRemoveDislike
{
    public class AddOrRemoveDislikeCommandHandler(IProductStatusRepository statusRepository, IUnitOfWorkPattern unitOfWorkPattern) : IRequestHandler<AddOrRemoveDislikeCommand, ErrorOr<Success>>
    {
        private readonly IProductStatusRepository _statusRepository = statusRepository;
        private readonly IUnitOfWorkPattern _unitOfWorkPattern = unitOfWorkPattern;
        public async Task<ErrorOr<Success>> Handle(AddOrRemoveDislikeCommand request, CancellationToken cancellationToken)
        {
            ProductStatus? productStatus;
            productStatus = await _statusRepository.GetProductStatus(request.ProductId, request.UserId);

            if(productStatus == null)
            {
                productStatus = ProductStatus.CreateStatus(request.ProductId, request.UserId);
                productStatus.AddOrRemoveDislike(request.Dislike);

                await _statusRepository.AddProductStatus(productStatus);
                await _unitOfWorkPattern.CommitChangesAsync();

                return Result.Success;
            }

            productStatus.AddOrRemoveDislike(request.Dislike);
            await _unitOfWorkPattern.CommitChangesAsync();

            return Result.Success;
        }
    }
}
