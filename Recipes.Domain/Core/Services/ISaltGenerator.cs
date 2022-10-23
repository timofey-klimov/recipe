namespace Recipes.Domain.Core.Services
{
    public interface ISaltGenerator
    {
        string Generate(string email, string login);
    }
}
