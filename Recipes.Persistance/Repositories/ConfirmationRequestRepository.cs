using Microsoft.EntityFrameworkCore;
using Recipes.Domain.Entities;
using Recipes.Domain.Enumerations;
using Recipes.Domain.Repositories;
using Recipes.Persistance.Repositories.Core;

namespace Recipes.Persistance.Repositories
{
    public class ConfirmationRequestRepository : AggregateRootRepository<ConfirmationRequest, Guid>, IConfirmationRequestRepository
    {
        public ConfirmationRequestRepository(ApplicationDbContext applicationDbContext) 
            : base(applicationDbContext)
        {
        }

        public async Task<IReadOnlyCollection<ConfirmationRequest>> GetByUserIdAsync(int userId, CancellationToken token = default)
        {
            return await Entities().Where(x => x.CreatedBy == userId).ToListAsync(token);
        }

        public async Task<IReadOnlyCollection<ConfirmationRequest>> GetForManulApproveAsync(CancellationToken token = default)
        {
            return await Entities()
                .Where(x => x.Status == ConfirmationRequestStatus.Pending &&
                        x.CheckType == ConfirmationRequestCheckType.Manual)
                .ToListAsync();
        }
    }
}
