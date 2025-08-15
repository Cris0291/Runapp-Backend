using MediatR;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.CustomerProfiles.Queries.GetUserLikes
{
    public class GetUserLikesQueryHandler(ILeftJoinRepository leftJoinRepository) : IRequestHandler<GetUserLikesQuery, IEnumerable<ProductUserLikesDto>>
    {
        private readonly ILeftJoinRepository _leftJoinRepository = leftJoinRepository;
        public async Task<IEnumerable<ProductUserLikesDto>> Handle(GetUserLikesQuery request, CancellationToken cancellationToken)
        {
            var result = _leftJoinRepository.GetProductsAndStatusLeftJoin(request.UserId);
            return await _leftJoinRepository.ExecuteQuery(result);
        }
    }
}
