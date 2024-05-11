using PrintManager.Logic.Models;

namespace PrintManager.Logic.Stores;

public interface IInstallationStore
{
    Task<Installation> CreateAsync(Installation installation);

    Task DeleteAsync(int id);

    Task<Installation?> GetByIdAsync(int id);

    Installation? GetById(int id);

    Task<IReadOnlyList<Installation>> GetByPageAsync(int skip, int pageSize, string branchName);

    Task<Installation?> GetByProperties(string installationName, int branchId, int printerId, int printerOrder);

    Task<int?> GetMaxPrinterOrderByInstallationNameAsync(string installationName, int branchId);

    Task<bool> DefaultInstallationExistsInBranchAsync(string installationName, int branchId);

    Task<bool> IsFirstInstallationInBranchAsync(string installationName, int branchId);
}
