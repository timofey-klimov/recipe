namespace Recipes.Application.Core.Auth
{
    public interface ICurrentUserProvider
    {
        int? UserId { get; }
    }
}
