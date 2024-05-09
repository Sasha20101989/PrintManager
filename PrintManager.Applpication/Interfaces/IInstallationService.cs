using PrintManager.Logic.Models;

namespace PrintManager.Applpication.Interfaces;

public interface IInstallationService
{
    Task<Installation> CreateAsync(string installationName, int branchId, int printerId, bool defaultInstallation, int? printerOrder);

    Task DeleteAsync(int id);

    Task<IReadOnlyList<Installation>> GetByBranchNameAsync(string branchName, int? page, int? pageSize);

    Task<Installation?> GetByIdAsync(int id);
}
