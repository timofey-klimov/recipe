namespace Recipes.Domain.Core.Repositories
{
    public interface ITransaction : IDisposable
    {
        Task CommitAsync();
        Task RollbackAsync();
    }
}
