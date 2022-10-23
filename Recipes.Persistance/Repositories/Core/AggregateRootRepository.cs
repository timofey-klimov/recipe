using Microsoft.EntityFrameworkCore;
using Recipes.Domain.Core;
using Recipes.Domain.Core.Repositories;

namespace Recipes.Persistance.Repositories.Core
{
    public abstract class AggregateRootRepository<T> : EntityRepository<T>, IAggregateRootRepository<T>
        where T : AggregateRoot
    {
        protected AggregateRootRepository(ApplicationDbContext applicationDbContext) 
            : base(applicationDbContext)
        {
        }

        public async Task<T?> GetByIdAsync(int id, CancellationToken token = default)
        {
            return await DbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id, token);
        }
    }
}
