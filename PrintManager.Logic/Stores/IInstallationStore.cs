using PrintManager.Logic.Models;

namespace PrintManager.Logic.Stores;

public interface IInstallationStore
{
    Task<Installation> CreateAsync(Installation installation);

    Task DeleteAsync(int id);

    Task<Installation?> GetByIdAsync(int id);

    Task<int?> GetMaxPrinterOrderByInstallationNameAsync(string installationName);

    Task<IReadOnlyList<Installation>> GetByPageAsync(int skip, int pageSize, string branchName);

    Task<Installation?> GetByProperties(string installationName, int branchId, int printerId, int printerOrder);
}
