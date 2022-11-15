using Microsoft.Extensions.Caching.Memory;

namespace Recipes.Persistance.Repositories.Cache.Core
{
    public interface ICachingProvider
    {
        IMemoryCache MemoryCache();
    }
}
