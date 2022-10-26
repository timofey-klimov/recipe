using MediatR;
using Recipes.Application.Core.Pagination;
using Recipes.Contracts.Web;
using Recipes.Domain.Core;

namespace Recipes.Application.Shared.Handlers
{
    public abstract class GetCollectionRequestHandler<TRequest, TResponse, TEntity> 
        : IRequestHandler<TRequest, PaginationResponse<TResponse>>
        where TRequest : IRequest<PaginationResponse<TResponse>>
        where TEntity : Entity
    {
        protected IPaginationProvider<TEntity> PaginationProvider;
        public GetCollectionRequestHandler(IPaginationProvider<TEntity> paginationProvider)
        {
            PaginationProvider = paginationProvider;
        }

        public async Task<PaginationResponse<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            return await GetColllectionAsync(request, cancellationToken);
        }

        public abstract Task<PaginationResponse<TResponse>> GetColllectionAsync(TRequest request, CancellationToken cancellationToken);
    }
}
