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

        public virtual async Task<T?> GetByIdAsync(int id, CancellationToken token = default)
        {
            return await DbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id, token);
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync() => await Entities().ToListAsync();

        public async Task<IReadOnlyCollection<T>> GetWithIncludesAsync(Specification<T>? spec = null, params string[] includes)
        {
            IQueryable<T> query = Entities();
            var queryWithIncludes = includes
                    .Aggregate(query, (current, s) => current.Include(s));

            if (spec is null)
            {
                return await queryWithIncludes
                    .ToListAsync();
            }
            else
            {
                return await queryWithIncludes
                    .Where(spec.Criteria())
                    .ToListAsync();
            }
        }
    }
}
