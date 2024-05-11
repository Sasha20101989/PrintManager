using PrintManager.Logic.Models;

namespace PrintManager.Applpication.Interfaces;

public interface IInstallationService
{
    Task<Installation> CreateAsync(string installationName, int branchId, int printerId, bool defaultInstallation, int? printerOrder);

    Task DeleteAsync(Installation installationToDelete);

    Task<Installation?> GetByIdAsync(int id);

    Installation? GetById(int id);

    Task<Installation?> GetByProperties(string installanionName, int branchId, int printerId, int printerOrder);

    Task<Installation> GetDefaultInstallationByBranchNameAsync(string branchName);

    Task<IReadOnlyList<Installation>> GetByBranchNameAsync(string branchName, int? page = null, int? pageSize = null);
}
