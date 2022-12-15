using Microsoft.EntityFrameworkCore;
using Recipes.Domain.Core;
using Recipes.Domain.Core.Repositories;

namespace Recipes.Persistance.Repositories.Core
{
    public abstract class AggregateRootRepository<TEntity, TId> : EntityRepository<TEntity, TId>, IAggregateRootRepository<TEntity, TId>
        where TEntity : AggregateRoot<TId>
        where TId : IEquatable<TId>
    {

        protected AggregateRootRepository(ApplicationDbContext applicationDbContext) 
            : base(applicationDbContext)
        {
        }

        public virtual async Task<TEntity?> GetByIdAsync(TId id, CancellationToken token = default)
        {
            return await DbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id.Equals(id), token);
        }

        public async Task<IReadOnlyCollection<TEntity>> GetAllAsync() => await Entities().ToListAsync();

        public async Task<IReadOnlyCollection<TEntity>> GetWithIncludesAsync(Specification<TEntity>? spec = null, params string[] includes)
        {
            IQueryable<TEntity> query = Entities();
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
