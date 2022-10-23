namespace Recipes.Domain.Core.Repositories
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken token = default);
    }
}
