using Recipes.Domain.Core.Repositories;
using Recipes.Domain.Entities;

namespace Recipes.Domain.Repositories
{
    public interface IConfirmationRequestRepository : IAggregateRootRepository<ConfirmationRequest, Guid>
    {
        Task<IReadOnlyCollection<ConfirmationRequest>> GetByUserIdAsync(int userId, CancellationToken token = default);

        Task<IReadOnlyCollection<ConfirmationRequest>> GetForManulApproveAsync(CancellationToken token = default);
    }
}
