using PrintManager.Logic.Models;

namespace PrintManager.Applpication.Interfaces;

public interface IInstallationService
{
    Task<IReadOnlyList<Installation>> GetByBranchNameAsync(string branchName, int? page, int? pageSize);

    Task<Installation?> GetByIdAsync(int id);
}
