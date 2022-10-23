using Recipes.Domain.Core;

namespace Recipes.Persistance.Repositories.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> SaveChangesAsync(CancellationToken token = default)
        {
            return await _dbContext.SaveChangesAsync(token);
        }
    }
}
