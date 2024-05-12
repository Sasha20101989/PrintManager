using PrintManager.Logic.Models;

namespace PrintManager.Application.Interfaces
{
    public interface IInstallationMemoryCache
    {
        void SetInstallations(IReadOnlyList<Installation> installations);

        void SetInstallation(Installation installation);

        IReadOnlyList<Installation> GetInstallations();

        IReadOnlyList<Installation>? GetInstallationsByPage(int skip, int size, string branchName);

        Installation? GetInstallationById(int id);     

        void RemoveInstallations();
    }
}
