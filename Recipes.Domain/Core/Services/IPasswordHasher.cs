namespace Recipes.Domain.Core.Services
{
    public interface IPasswordHasher
    {
        string Hash(string password, string salt);
    }
}
