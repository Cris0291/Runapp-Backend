using ErrorOr;
using MediatR;
using RunApp.Domain.ProductStatusAggregate;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.ProductStatuses.Commands.AddOrRemoveLike
{
    public class AddOrRemoveLikeCommandHandler(IProductStatusRepository productStatusRepository, IUnitOfWorkPattern unitOfWork) : IRequestHandler<AddOrRemoveLikeCommand, ErrorOr<Success>>
    {

        private readonly IProductStatusRepository _productStatusRepository = productStatusRepository;
        private readonly IUnitOfWorkPattern _unitOfWork = unitOfWork;
        public async Task<ErrorOr<Success>> Handle(AddOrRemoveLikeCommand request, CancellationToken cancellationToken)
        {
            ProductStatus? productStatus;
            productStatus = await _productStatusRepository.GetProductStatus(request.ProductId, request.UserId);
            if (productStatus == null)
            {
                productStatus = ProductStatus.CreateStatus(request.ProductId, request.UserId);
                productStatus.AddOrRemoveLike(request.Like);
                await _productStatusRepository.AddProductStatus(productStatus);

                await _unitOfWork.CommitChangesAsync();

                return Result.Success;
            }
            if (productStatus.Like == request.Like) return Error.Validation(code: "ProductWasAlreadyLikeOrDislikeByCurrentUser", description: "Product was already like or dislike by current user");

            productStatus.AddOrRemoveLike(request.Like);

            await _unitOfWork.CommitChangesAsync();

            return Result.Success;
        }
    }
}
