using PrintManager.Logic.Models;

namespace PrintManager.Logic.Stores;

public interface IInstallationStore
{
    Task<Installation?> GetByIdAsync(int id);

    Task<IReadOnlyList<Installation>> GetByPageAsync(int skip, int pageSize, string branchName);
}
