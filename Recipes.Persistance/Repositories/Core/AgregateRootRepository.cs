using Microsoft.EntityFrameworkCore;
using Recipes.Domain.Core;

namespace Recipes.Persistance.Repositories.Core
{
    public abstract class AgregateRootRepository<T> : EntityRepository<T>, IAgregateRootRepository<T>
        where T : AgregateRoot
    {
        protected AgregateRootRepository(ApplicationDbContext applicationDbContext) 
            : base(applicationDbContext)
        {
        }

        public async Task<T?> GetByIdAsync(int id, CancellationToken token = default)
        {
            return await DbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id, token);
        }
    }
}
