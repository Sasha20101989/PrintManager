using Microsoft.Extensions.Caching.Memory;
using PrintManager.Applpication.Interfaces;
using PrintManager.Logic.Models;

namespace PrintManager.Applpication.Services
{
    public class InstallationMemoryCache(IMemoryCache memoryCache) : IInstallationMemoryCache
    {
        private const string CacheKey = "Installations";

        private readonly TimeSpan CacheLifeTime = TimeSpan.FromHours(1).Add(TimeSpan.FromMinutes(45));

        public IReadOnlyList<Installation>? GetInstallations()
        {
            return memoryCache.Get<IReadOnlyList<Installation>>(CacheKey);
        }

        public void RemoveInstallations()
        {
            memoryCache.Remove(CacheKey);
        }

        public void SetInstallations(IReadOnlyList<Installation> installations)
        {
            
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(CacheLifeTime);

            memoryCache.Set(CacheKey, installations, cacheEntryOptions);
        }
    }
}
