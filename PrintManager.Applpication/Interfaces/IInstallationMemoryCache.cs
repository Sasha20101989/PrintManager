using PrintManager.Logic.Models;

namespace PrintManager.Applpication.Interfaces
{
    public interface IInstallationMemoryCache
    {
        void SetInstallations(IReadOnlyList<Installation> installations);

        IReadOnlyList<Installation> GetInstallations();

        void RemoveInstallations();
    }
}
