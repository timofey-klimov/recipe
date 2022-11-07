using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Recipes.Application.Core.Files;

namespace Recipes.Infrustructure.Files
{
    public class PhysicalFileProvider : IPhysicalFileProvider
    {
        private readonly IHostingEnvironment _environment;
        private const string ImagesFolder = "images";
        public PhysicalFileProvider(IHostingEnvironment environment)
        {
            _environment = environment;
        }
        public async Task<string> SaveFileAsync(IFormFile formFile, CancellationToken cancellationToken)
        {
            var webRootPath = _environment.WebRootPath;

            var imagesRootPath = Path.Combine(webRootPath, ImagesFolder);

            if (!Directory.Exists(Path.Combine(imagesRootPath)))
                Directory.CreateDirectory(imagesRootPath);

            var guid = Guid.NewGuid();
            var imageName = $"{guid}{Path.GetExtension(formFile.FileName)}";

            using var fileStream = new FileStream(Path.Combine(imagesRootPath, imageName), FileMode.Create);

            await formFile.CopyToAsync(fileStream, cancellationToken);

            return $"/{ImagesFolder}/{imageName}";
        }
    }
}
