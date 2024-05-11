using PrintManager.Logic.Models;

namespace PrintManager.Applpication.Interfaces;

public interface IInstallationService
{
    Task<Installation> CreateAsync(string installationName, Branch branch, Printer printer, bool defaultInstallation, int? printerOrder);

    Task DeleteAsync(Installation installationToDelete);

    Task<IReadOnlyList<Installation>> GetByBranchNameAsync(string branchName, int? page, int? pageSize);

    Task<Installation?> GetByIdAsync(int id);

    Task<Installation?> GetByProperties(string installanionName, int branchId, int printerId, int printerOrder);
}
