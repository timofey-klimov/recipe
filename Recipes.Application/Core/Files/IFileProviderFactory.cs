namespace Recipes.Application.Core.Files
{
    public interface IFileProviderFactory
    {
        IPhysicalFileProvider GetPhysicalFileProvider();

        IDbFileProvider GetDbFileProvider();
    }
}
