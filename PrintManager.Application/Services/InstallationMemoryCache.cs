using Microsoft.Extensions.Caching.Memory;
using PrintManager.Application.Interfaces;
using PrintManager.Logic.Models;

namespace PrintManager.Application.Services
{
    public class InstallationMemoryCache(IMemoryCache memoryCache) : IInstallationMemoryCache
    {
        private const string CacheKey = "Installations";

        private readonly TimeSpan CacheLifeTime = TimeSpan.FromHours(1).Add(TimeSpan.FromMinutes(45));

        public IReadOnlyList<Installation>? GetInstallations()
        {
            return memoryCache.Get<IReadOnlyList<Installation>>(CacheKey);
        }

        public IReadOnlyList<Installation>? GetInstallationsByPage(int skip, int size, string branchName)
        {
            IReadOnlyList<Installation>? installations = GetInstallations();

            if(installations is null)
            {
                return null;
            }

            return installations
            .Where(i =>i.Branch is not null && !string.IsNullOrWhiteSpace(i.Branch.BranchName) && i.Branch.BranchName.Equals(branchName, StringComparison.OrdinalIgnoreCase))
            .Skip(skip)
            .Take(size)
            .ToList();
        }

        public Installation? GetInstallationById(int id)
        {
            IReadOnlyList<Installation>? installations = GetInstallations();

            if (installations is null)
            {
                return null;
            }

            return installations.FirstOrDefault(i => i.InstallationId == id);
        }

        public void RemoveInstallations()
        {
            memoryCache.Remove(CacheKey);
        }

        public void SetInstallations(IReadOnlyList<Installation> installations)
        {

            MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(CacheLifeTime);

            memoryCache.Set(CacheKey, installations, cacheEntryOptions);
        }

        public void SetInstallation(Installation installation)
        {
            IReadOnlyList<Installation>? installations = GetInstallations();

            if (installations != null)
            {
                List<Installation> updatedInstallations = installations.ToList();

                Installation? existingInstallation = updatedInstallations.FirstOrDefault(i => i.InstallationId == installation.InstallationId);

                if (existingInstallation != null)
                {
                    updatedInstallations.Remove(existingInstallation);
                }

                updatedInstallations.Add(installation);

                SetInstallations(updatedInstallations);
            }
        }
    }
}
