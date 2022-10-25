using Recipes.Contracts.Services.Files;

namespace Recipes.Application.Core.Files
{
    public interface IFileProvider
    {
        Task<UploadFile> CreateUploadFileAsync(string contentType, string? fileName, long size, Stream fileContent, CancellationToken token = default);
    }
}
