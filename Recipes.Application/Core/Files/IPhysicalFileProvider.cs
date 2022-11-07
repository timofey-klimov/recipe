using Microsoft.AspNetCore.Http;

namespace Recipes.Application.Core.Files
{
    public interface IPhysicalFileProvider
    {
        Task<string> SaveFileAsync(IFormFile formFile, CancellationToken cancellationToken);
    }
}
