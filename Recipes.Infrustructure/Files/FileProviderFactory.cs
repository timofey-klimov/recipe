using Microsoft.Extensions.DependencyInjection;
using Recipes.Application.Core.Files;

namespace Recipes.Infrustructure.Files
{
    public class FileProviderFactory : IFileProviderFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public FileProviderFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IDbFileProvider GetDbFileProvider() =>
            _serviceProvider.GetRequiredService<IDbFileProvider>();

        public IPhysicalFileProvider GetPhysicalFileProvider() =>
            _serviceProvider.GetRequiredService<IPhysicalFileProvider>();
        
    }
}
