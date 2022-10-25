using Recipes.Application.Core.Files;
using Recipes.Contracts.Services.Files;

namespace Recipes.Infrustructure.Files
{
    public class FilesInDbProvider : IFileProvider
    {
        public async Task<UploadFile> CreateUploadFileAsync(string contentType, string? fileName, long size, Stream fileContent, CancellationToken token = default)
        {
            var ms = new MemoryStream();
            await fileContent.CopyToAsync(ms);

            return new UploadFile(contentType, fileName, size, ms.ToArray());
        }
    }
}
