using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Recipes.Persistance.Repositories.Cache.Core;

namespace Recipes.Infrustructure.Caching
{
    public class CachingProvider : ICachingProvider
    {
        private readonly IServiceProvider _serviceProvider;
        public CachingProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IMemoryCache MemoryCache() => _serviceProvider.GetRequiredService<IMemoryCache>();
        
    }
}
